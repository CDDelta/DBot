using System;
using System.IO;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DBot
{
  class Program
  {
    static void Main(string[] args)
    {
      foreach (var mapPath in Directory.GetFiles("test_depth_maps"))
      {
        using (var depthMap = Image.Load<L8>(mapPath))
        using (var file = new StreamWriter($"point_clouds/{Path.GetFileNameWithoutExtension(mapPath)}.txt"))
        {
          for (int y = 0; y < depthMap.Height; y += 5)
          {
            var pixelRowSpan = depthMap.GetPixelRowSpan(y);
            for (int x = 0; x < depthMap.Width; x += 5)
            {
              // The relative straight line distance of this pixel from the camera.
              var relativeDepth = pixelRowSpan[x];

              var point = new Vector3();

              file.WriteLine($"{point.X}, {point.Y}, {point.Z}");
            }
          }
        }
      }
    }
  }
}
