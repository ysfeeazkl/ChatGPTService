using OpenAI_API;
using OpenAI_API.Chat;
using System;

namespace ChatGPTService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        OpenAIAPI api = new OpenAI_API.OpenAIAPI(new APIAuthentication("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", "org-xZSDl8bqs3GPFdQRNBs3HUd8"));

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //var api = new OpenAI_API.OpenAIAPI("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");
            //var api = new OpenAI_API.OpenAIAPI(new APIAuthentication("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", "org-xZSDl8bqs3GPFdQRNBs3HUd8"));
            var result = await api.Completions.GetCompletion("test");


            await TestChadv2();



        }

        public async Task Test1()
        {
            for (; ; )
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                await Console.Out.WriteLineAsync("\nSoru sor: \n");
                Console.BackgroundColor = ConsoleColor.Black;

                var input = Console.ReadLine();
                await Console.Out.WriteLineAsync();

                var chat = api.Chat.CreateConversation();
                chat.AppendUserInput(input.ToString());

                Console.BackgroundColor = ConsoleColor.Red;
                await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                {
                    Console.Write(res);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                await Console.Out.WriteLineAsync();
            }
        }

        public async Task TestChad()
        {
            var chat = api.Chat.CreateConversation();

            
            chat.AppendSystemMessage("Çocukların nesnelerin hayvan olup olmadığını anlamalarına yardımcı olan bir öğretmensiniz. Kullanıcı size hayvan derse \"evet\" dersiniz. Kullanıcı size hayvan olmayan bir şey söylerse \"hayır\" dersiniz. Yalnızca \"evet\" veya \"hayır\" şeklinde yanıt verirsiniz. Başka bir şey söylemezsiniz.");

            chat.AppendUserInput("Bu bir hayvan mı? Kedi");
            chat.AppendExampleChatbotOutput("Evet");
            chat.AppendUserInput("Bu bir hayvan mı? Ev");
            chat.AppendExampleChatbotOutput("Hayır");

           
            chat.AppendUserInput("Bu bir hayvan mı? Köpek");
  
            string response = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine(response); 

          
            chat.AppendUserInput("Bu bir hayvan mı? Sandalye");
            // and get another response
            response = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine(response); 

            foreach (ChatMessage msg in chat.Messages)
            {
                Console.WriteLine($"{msg.Role}: {msg.Content}");
            }
        }

        public async Task TestChadv2()
        {
            var chat = api.Chat.CreateConversation();

            await ColorfulFontDarkRed("\nBir olay örgüsü anlatın,ne yapmasını istediğinizle alakalı örnekler verin. ve soru sorun\n");
            chat.AppendSystemMessage(Console.ReadLine());

            await ColorfulFontDarkRed("\n2 tane örnek giriniz\n");


            await ColorfulFontRed("1. soru");
            chat.AppendUserInput(Console.ReadLine());
            await ColorfulFontRed("2. cevap");
            chat.AppendExampleChatbotOutput(Console.ReadLine());
            await ColorfulFontRed("2. soru");
            chat.AppendUserInput(Console.ReadLine());
            await ColorfulFontRed("2. Cevap");
            chat.AppendExampleChatbotOutput(Console.ReadLine());


            for (; ;)
            {
                await Console.Out.WriteLineAsync("\nŞimdi bir soru sorun\n");
                chat.AppendUserInput(Console.ReadLine());
                string response = await chat.GetResponseFromChatbotAsync();

                await ColorfulFontRed(response);
            }
          
        }

        public async Task ColorfulFontDarkRed(string write)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            await Console.Out.WriteLineAsync(write);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public async Task ColorfulFontRed(string write)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync(write);
            Console.BackgroundColor = ConsoleColor.Black;
        }


    }
}