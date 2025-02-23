using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessingWithCode
{
    class VSAudioReplacer
    {
        static string GameSoundsFolderPath = Path.Combine("Vintagestory", "assets", "survival", "sounds");
        static string ModifiedSoundsFolderPath = Path.Combine("VSCustomSounds", "AudioToReplace", "sounds");

        static List<string> allDirectories = new List<string>();
        static List<string> allFiles = new List<string>();

        static void CheckIfPathsExist()
        {
            bool missingDirectories = false;

            if (!Directory.Exists(GameSoundsFolderPath))
            {
                Console.WriteLine($"Directory: {GameSoundsFolderPath} does not exist");
                Console.WriteLine($"Creating new directory");
                Console.WriteLine($"(This will most likely not work if the actual sound files for the game are not where they are expected to be)");
                Directory.CreateDirectory(GameSoundsFolderPath);
                missingDirectories = true;
            }

            if (!Directory.Exists(ModifiedSoundsFolderPath))
            {
                Console.WriteLine($"Directory: {ModifiedSoundsFolderPath} does not exist");
                Console.WriteLine($"Creating new directory");
                Console.WriteLine($"(This will most likely not work if there are no files to replace the original ones)");
                Directory.CreateDirectory(ModifiedSoundsFolderPath);
                missingDirectories = true;
            }

            if (missingDirectories)
            {
                Console.WriteLine();
                Console.WriteLine($"Some direcotries were missing");
                Console.WriteLine($"New ones were created but that wont have any affect if one of the two werent there before");
                Console.WriteLine($"The point is to be able to modify the audio in the {ModifiedSoundsFolderPath}");
                Console.WriteLine($"and then use this program to replace the original files at: {GameSoundsFolderPath}");
                Console.WriteLine($"so when an update comes out that overwrites the modified files in the original game folder (at: {GameSoundsFolderPath})");
                Console.WriteLine($"it wouldnt then take ages to find and replace all the files again because this program would do it");
                Console.WriteLine();
            }
        }

        static void EnsureExistingPath(string path)
        {
            FileAttributes attr = File.GetAttributes(path); // get the file attributes for file or directory

            if (attr.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine($"{path} is a directory");

                allDirectories.Add(path);

                Console.WriteLine($"\nDirectories in {path}:");
                foreach (var dir in Directory.GetDirectories(path))
                {
                    EnsureExistingPath(dir);
                }

                Console.WriteLine($"\nFiles in {path}:");
                foreach (var file in Directory.GetFiles(path))
                {
                    EnsureExistingPath(file);
                }
            }
            else
            {
                Console.WriteLine($"{path} is a file");

                allFiles.Add(path);

                //File.Copy(path, GameSoundsFolderPath, true);
            }
        }

        public static void ReplaceAudio()
        {
            CheckIfPathsExist();

            Console.WriteLine($"Directories in {ModifiedSoundsFolderPath}:");
            foreach (var dir in Directory.GetDirectories(ModifiedSoundsFolderPath))
            {
                EnsureExistingPath(dir);
            }

            Console.WriteLine($"Files in {ModifiedSoundsFolderPath}:");
            foreach (var file in Directory.GetFiles(ModifiedSoundsFolderPath))
            {
                EnsureExistingPath(file);
            }

            Console.WriteLine($"\n\n\nALL FOLDERS AND FILES:\n\n\n");
            foreach (var dir in allDirectories)
            {
                string newDir = Path.Combine(GameSoundsFolderPath, dir.Replace(ModifiedSoundsFolderPath + "\\", ""));

                if (!Directory.Exists(newDir))
                {
                    Directory.CreateDirectory(newDir);

                    Console.WriteLine($"{newDir}\t\t\t(+)");
                }
                else
                {
                    Console.WriteLine($"{newDir}");
                }

                
            }
            Console.WriteLine();
            foreach (var file in allFiles)
            {
                string newFile = Path.Combine(GameSoundsFolderPath, file.Replace(ModifiedSoundsFolderPath + "\\", ""));

                Console.WriteLine($"{newFile}");

                File.Copy(file, newFile, true);
            }
        }
    }
}
