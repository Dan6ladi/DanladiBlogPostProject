using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Infrastructure.Model
{
    public class EmailTemplateHelper
    {

        public static string WelcomeMailTemplate(string customerName)
        {
            var mailTemplate = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Welcome to Danladi Blog Post Management - Registration Successful!</title>
</head>
<body>
    <h1>Danladi Blog Post Management - Registration Successful!</h1>
    <p>
        Dear {customerName},
    </p>
    <p>
        We are excited to welcome you to Danladi Blog Post Management. We are thrilled that you have chosen to become a part of our community.
    </p>
    <p>
        Your registration was successful, and you are now all set to explore our platform's exciting features. Here's what you can look forward to:
    </p>
    <ul>
        <li>Convenient Appointment Booking: Easily schedule appointments for tags and title services, tailored to your preferences.</li>
        <li>Transparent Pricing: Access information on our services and pricing to make informed decisions.</li>
        <li>Dedicated Support: Our team is here to assist you every step of the way. If you have any questions or need assistance, please don't hesitate to reach out.</li>
        <li>Exclusive Offers: Stay tuned for special offers and promotions designed just for you.</li>
    </ul>
    <p>
        Thank you for choosing Danladi Blog Post Management. We value your trust and look forward to serving you. If you have any feedback or suggestions, please feel free to share. We are continuously working to enhance your experience.
    </p>
    <p>
        Get started today by logging in to your account at our website.
    </p>
    <p>
        Once again, welcome to Danladi Blog Post. We're here to make your blogging smooth and stress-free.
    </p>
    <p>
        Best regards
    </p>
</body>
</html>";

            return mailTemplate;
        }

    }
}
