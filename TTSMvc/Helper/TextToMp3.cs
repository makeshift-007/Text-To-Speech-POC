using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.IO;
using System.Linq;
//using Microsoft.Speech.Synthesis;
using System.Speech.Synthesis;
using NAudio.Lame;
using NAudio.Wave;

/// <summary>
/// Summary description for TextToMp3
/// </summary>
public class TextToMp3Synthesis
{

    public Stream Convert(string text)
    {
        using (var wavStream = new MemoryStream())
        using (var synthesizer = new SpeechSynthesizer())
        {
            synthesizer.SetOutputToWaveStream(wavStream);
            synthesizer.Speak(text);
            synthesizer.SetOutputToNull();
            return ConvertWavToMp3(wavStream);
        }
    }

    public static void CheckAddBinPath()
    {
        // find path to 'bin' folder
        var binPath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, "bin" });
        // get current search path from environment
        var path = Environment.GetEnvironmentVariable("PATH") ?? "";

        // add 'bin' folder to search path if not already present
        if (!path.Split(Path.PathSeparator).Contains(binPath, StringComparer.CurrentCultureIgnoreCase))
        {
            path = string.Join(Path.PathSeparator.ToString(), new string[] { path, binPath });
            Environment.SetEnvironmentVariable("PATH", path);
        }
    }

    public MemoryStream ConvertWavToMp3(MemoryStream wavStream)
    {
        CheckAddBinPath();
        //rewind to beginning of stream
        wavStream.Seek(0, SeekOrigin.Begin);

        var mp3Stream = new MemoryStream();
        var waveFileReader = new WaveFileReader(wavStream);
        var lameMp3FileWriter = new LameMP3FileWriter(mp3Stream, waveFileReader.WaveFormat, LAMEPreset.VBR_90);

        waveFileReader.CopyTo(lameMp3FileWriter);
        mp3Stream.Seek(0, SeekOrigin.Begin);
        return mp3Stream;

    }
}
