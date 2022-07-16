﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchAutoHighlighter
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            Console.WriteLine("Give me ID");
            var id = Console.ReadLine();
            
            Console.WriteLine("Give me highlights count");
            var count = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Highlights duration");
            var hSeconds = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Downloading chat...");
            Downloader.DownloadChat(id);
            var chatAnalyzer = new ChatAnalyzer();
            var top = chatAnalyzer.Analyze(count, hSeconds);
            
            Console.WriteLine("Downloading highlights");
            for (int i = 1; i <= top.Count; i++)
            {
                var secondHighlight = top[i - 1].Item1 - 5;
                Downloader.DownloadVideo(id, i,secondHighlight - hSeconds, secondHighlight);

            }
            
            Console.WriteLine("Making main video");
            var cutter = new VideoCutter();
            cutter.Cut(count);
            Console.WriteLine("Deleting highlights");
            cutter.DeleteHighlights();
            Console.WriteLine("DONE");




        }
    }
}

