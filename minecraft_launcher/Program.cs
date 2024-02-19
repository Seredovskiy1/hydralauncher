using System;
using System.IO;
using Newtonsoft.Json;
using CmlLib.Core;
using CmlLib.Core.Auth;

namespace minecraft_launcher
{
    class MainClass
    {
        static string profileDirectory = "profile";
        static string profileFilePath = Path.Combine(profileDirectory, "profile.json");

        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (!Directory.Exists(profileDirectory))
                Directory.CreateDirectory(profileDirectory);

            string username = "";
            string lastVersion = "";

            if (File.Exists(profileFilePath))
            {
                string json = File.ReadAllText(profileFilePath);
                dynamic profile = JsonConvert.DeserializeObject(json);
                if (profile != null)
                {
                    username = profile.username;
                    lastVersion = profile.lastVersion;
                }
            }


            Console.WriteLine("Type your nickname" + (string.IsNullOrEmpty(username) ? "" : " [DEFAULT: " + username + "]") + ": ");
            var inputUsername = Console.ReadLine().Trim();

            // Використання введеного нікнейму або збереженого раніше
            username = string.IsNullOrEmpty(inputUsername) ? username : inputUsername;


            Console.WriteLine("Type minecraft version" + (string.IsNullOrEmpty(lastVersion) ? "" : " [DEFAULT: " + lastVersion + "]") + ": ");
            var inputVersion = Console.ReadLine().Trim();

            // Використання введеної версії або збереженої раніше
            string versionSelected = string.IsNullOrEmpty(inputVersion) ? lastVersion : inputVersion;

            // Збереження нікнейму та версії гри у файлі JSON
            dynamic profileObject = new { username = username, lastVersion = versionSelected };
            string profileJson = JsonConvert.SerializeObject(profileObject);
            File.WriteAllText(profileFilePath, profileJson);

            // Конфігурація CMLauncher
            System.Net.ServicePointManager.DefaultConnectionLimit = 256;
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            var defaultVersion = "1.8.9";

            // Отримання списку доступних версій гри
            var versions = await launcher.GetAllVersionsAsync();
            foreach (var v in versions)
            {
                Console.WriteLine(v.Name);
            }

            // Використання версії гри для створення процесу гри
            versionSelected = string.IsNullOrWhiteSpace(versionSelected) ? defaultVersion : versionSelected;

            var process = await launcher.CreateProcessAsync(versionSelected, new MLaunchOption
            {
                Session = MSession.CreateOfflineSession(username),
                MaximumRamMb = 2048,
                VersionType = "HydraLauncher",
                GameLauncherName = "HydraLauncher",
                GameLauncherVersion = "v.1.0",
                FullScreen = false,
            });

            process.Start();
        }
    }
}
