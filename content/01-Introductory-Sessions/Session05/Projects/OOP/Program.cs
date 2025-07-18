using OOP;

User user = new User();
Console.WriteLine(user.Introduce());

ISender sender = new EmailSender();
ISender sender2 = new SMSSender();


//SMS
user.SendMessageToSomeone("H", new SMSSender());




//Email
user.SendMessageToSomeone("H", new EmailSender());