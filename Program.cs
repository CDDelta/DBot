using System;
using System.IO;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DBot
{
  class Program
  {
    /// <summary>
    /// The horizontal FOV of the depth map source camera.
    /// </summary>
    const float FOV_H = (100f / 180f) * MathF.PI;
    /// <summary>
    /// The angle at which the leftmost sample in the depth map starts at.
    /// </summary>
    const float H_START_ANGLE = (MathF.PI - FOV_H) / 2;

    /// <summary>
    /// The vertical FOV of the depth map source camera.
    /// </summary>
    const float FOV_V = (50f / 180f) * MathF.PI;
    /// <summary>
    /// The angle at which the topmost sample in the depth map starts at.
    /// </summary>
    const float V_START_ANGLE = (MathF.PI - FOV_V) / 2;

    static void Main(string[] args)
    {
      foreach (var mapPath in Directory.GetFiles("test_depth_maps"))
      {
        using (var depthMap = Image.Load<L8>(mapPath))
        using (var file = new StreamWriter($"point_clouds/{Path.GetFileNameWithoutExtension(mapPath)}.txt"))
        {
          for (int y = 0; y < depthMap.Height; y++)
          {
            var pixelRowSpan = depthMap.GetPixelRowSpan(y);
            for (int x = 0; x < depthMap.Width; x++)
            {
              // The relative distance of this pixel perpendicular to the camera plane.
              var relativeDepth = 255 - (float)pixelRowSpan[x].PackedValue;

              // The angle the point is at w.r.t the camera plane.
              var pointHAngle = H_START_ANGLE + ((float)x / depthMap.Width) * FOV_H;
              var pointVAngle = V_START_ANGLE + ((float)y / depthMap.Height) * FOV_V;

              // The coordinates the point is at relative to the source camera.
              var relativeX = relativeDepth / MathF.Tan(pointHAngle);
              var relativeY = relativeDepth / MathF.Tan(pointVAngle);

              // TODO: Determine whether the supplied depth maps show the straight line distance from the camera or the distance perpendicular to the camera plane.
              // relativeX = relativeDepth * (pointHAngle > MathF.PI / 2 ? MathF.Cos(MathF.PI - pointHAngle) : -MathF.Cos(pointHAngle));
              // relativeY = relativeDepth * MathF.Cos(pointVAngle);
              // relativeDepth = relativeDepth * MathF.Sin(pointHAngle);

              var point = new Vector3(relativeX, relativeY, relativeDepth);

              file.WriteLine($"{point.X}, {point.Y}, {point.Z}");
            }
          }
        }
      }
    }
  }
}
