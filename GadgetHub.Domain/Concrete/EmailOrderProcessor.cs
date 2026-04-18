using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;
using GadgetHub.WebUI.Models;
using GadgetHubs.Domain.Abstract;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GadgetHub.Domain.Concrete
{
	public class EmailOrderProcessor : IOrderProcessor
	{
		private EmailSettings emailSettings;

		public EmailOrderProcessor(EmailSettings settings)
		{
			emailSettings = settings;
		}

		public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
		{
			// Ensure directory exists
			Directory.CreateDirectory(emailSettings.FileLocation);

			using (var smtpClient = new SmtpClient())
			{
				smtpClient.Host = "localhost";
				smtpClient.Port = 25;
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
				smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;

				if (emailSettings.WriteAsFile)
				{
					smtpClient.EnableSsl = false;
				}

				StringBuilder body = new StringBuilder();

				body.AppendLine("New order submitted");
				body.AppendLine("---");
				body.AppendLine("Items:");

				foreach (var line in cart.Lines)
				{
					var subtotal = line.Gadget.Price * line.Quantity;
					body.AppendLine($"{line.Quantity} x {line.Gadget.Name} (subtotal: {subtotal:c})");
				}

				body.AppendLine($"Total order value: {cart.ComputeTotalValue():c}");
				body.AppendLine("---");
				body.AppendLine("Ship to:");
				body.AppendLine(shippingDetails.Name);
				body.AppendLine(shippingDetails.Line1);
				body.AppendLine(shippingDetails.Line2 ?? "");
				body.AppendLine(shippingDetails.City);
				body.AppendLine(shippingDetails.State);
				body.AppendLine(shippingDetails.Zip);
				body.AppendLine(shippingDetails.Country);
				body.AppendLine("---");
				body.AppendLine($"Gift wrap: {(shippingDetails.GiftWrap ? "Yes" : "No")}");

				MailMessage mailMessage = new MailMessage(
					"orders@gadgethub.com",
					"admin@gadgethub.com",
					"New Order Submitted",
					body.ToString()
				);

				smtpClient.Send(mailMessage);
			}
		}
	}
}