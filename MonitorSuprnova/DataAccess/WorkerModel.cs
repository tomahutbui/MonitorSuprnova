using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Linq;

namespace DataAccess
{
    public class WorkerModel
    {
        public static List<WorkerModel> BaseWorkers { get; set; } = new List<WorkerModel>();

        public static string TokenMMVNBOT = "547736169:AAEcDzshUI0MEfCyl31_9ulE7zVrydVoUw0";
        public static string ChatID = "406920267";
        public static int WaitMinutes = 10;
        //public static string TokenMMVNBOT = "503915341:AAG5pBsAJ6oMSFT0PW1xqOREX-2I9V35EEc";
        
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public long hashrate { get; set; }
        public long shares { get; set; }

        public bool IsMonitor { get; set; }

        public async static Task<List<User>> GetID_ByListUserName(List<User> users)
        {
            try
            {
                
                var bot = new TelegramBotClient(WorkerModel.TokenMMVNBOT);
                var updateds = await bot.GetUpdatesAsync();
                if (updateds != null && updateds.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        var founder = updateds.FirstOrDefault(x => x.Message.From.Username == user.UserName);
                        if (founder != null)
                            user.ID = founder.Message.Chat.Id.ToString();
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async static void OnStart()
        {
            WorkerModel.BaseWorkers = await DA.GetData();
            
            string mes = "[FIRST START]" + Environment.NewLine;
            foreach (var w in WorkerModel.BaseWorkers)
            {
                mes += $"{w.username} => {(w.hashrate.Equals(0) ? "STOPPED" : "STARTED")}{Environment.NewLine}";
                w.IsMonitor = w.hashrate > 0;
            }
                
            Console.WriteLine("[OnStart]" + Environment.NewLine + mes);
            await WorkerModel.SendMessage(mes);
        }

        public static async Task<bool> ProcessData()
        {
            var currentWorkers = await DA.GetData();
            string mes = "";
            foreach (var currentW in currentWorkers)
            {
                if (!WorkerModel.BaseWorkers.Any(x => x.id == currentW.id))
                {
                    if (currentW.hashrate > 0)
                    {
                        mes += $"{currentW.username} => STARTED{Environment.NewLine}";
                        currentW.IsMonitor = true;
                    }
                    else
                        currentW.IsMonitor = false;
                    WorkerModel.BaseWorkers.Add(currentW);
                    continue;
                }
                else
                {
                    var baseCurrent = WorkerModel.BaseWorkers.FirstOrDefault(x => x.id == currentW.id);
                    int index = WorkerModel.BaseWorkers.FindIndex(x => x.id == currentW.id);
                    if (!baseCurrent.IsMonitor)
                    {
                        if (currentW.hashrate > 0)
                        {
                            mes += $"{currentW.username} => STARTED{Environment.NewLine}";
                            currentW.IsMonitor = true;
                        }
                        WorkerModel.BaseWorkers[index] = currentW;
                        continue;
                    }
                    else
                    {
                        if (currentW.hashrate.Equals(0))
                        {
                            mes += $"{currentW.username} => STOPPED{Environment.NewLine}";
                            currentW.IsMonitor = false;
                        }
                        else currentW.IsMonitor = true;
                        WorkerModel.BaseWorkers[index] = currentW;
                        continue;
                    }
                }
            }
            if (!string.IsNullOrEmpty(mes))
            {
                Console.WriteLine(mes);
                await WorkerModel.SendMessage(mes);
            }
                
            return true;
        }

        public async static Task<bool> SendMessage(string text)
        {
            var bot = new TelegramBotClient(WorkerModel.TokenMMVNBOT);
            int indexUser = -1;
            try
            {
                
                var t = await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(WorkerModel.ChatID), text);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return true;
        }

        public static void aa()
        {
            var bot = new TelegramBotClient(WorkerModel.TokenMMVNBOT);
            bot.StartReceiving();
            bot.ApiResponseReceived += (sender, e) =>
            {

            };
            //bot.MakingApiRequest += Bot_MakingApiRequest;
            bot.OnCallbackQuery += (sender, e) =>
            {

            };
            bot.OnInlineQuery += (sender, e) =>
            {

            };
            bot.OnInlineResultChosen += (sender, e) =>
            {

            };
            bot.OnMessage += (sender, e) =>
            {

            };
            bot.OnMessageEdited += (sender, e) =>
            {

            };
            bot.OnUpdate += (sender, e) =>
            {

            };
        }
    }

    public class User
    {
        public string UserName { get; set; }
        public string ID { get; set; }
    }
}
