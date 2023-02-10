using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Threading;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)

    {
        var client = new TelegramBotClient("6243012420:AAETlY15vNF1QVbybWMxAz0Wzze8z72Uoog");
        client.StartReceiving(update, Error);
            Console.ReadLine();
    }

    async static Task update(ITelegramBotClient botClient, Update update, CancellationToken arg3)
    {
        var message = update.Message;
        if (message.Text !=null) 
        {
            if (message.Text.ToLower().Contains("погода питер"))
            {
                botClient.SendTextMessageAsync(message.Chat.Id, "Город: Санкт-Петербург, Скорость ветра: 4 м/с, температура: -1 градус, влажность: 74, давление:761");
                return;
            }

            if (message.Text.ToLower().Contains("погода Москва"))
            {
                botClient.SendTextMessageAsync(message.Chat.Id, "Город: Москва, Скорость ветра: 10м/с, температура: -5 градус, влажность: 70, давление:755");
                return;
            }

            if (message.Text.ToLower().Contains("погода Екатеринбург"))
            {
                botClient.SendTextMessageAsync(message.Chat.Id, "Город: Екатеринбург, Скорость ветра: 1м/с, температура: 0 градус, влажность: 35, давление:655");
                return;
            }
        }
    }


    async static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }
}