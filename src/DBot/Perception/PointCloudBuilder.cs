using System;
using MathNet.Numerics.LinearAlgebra;

namespace DBot.Perception
{
  public class PointCloudBuilder
  {
    /// <summary>
    /// The horizontal FOV of the depth map source camera.
    /// </summary>
    const double FOV_H = (100 / 180) * Math.PI;
    /// <summary>
    /// The angle at which the leftmost sample in the depth map starts at.
    /// </summary>
    const double H_START_ANGLE = (MathF.PI - FOV_H) / 2;

    /// <summary>
    /// The vertical FOV of the depth map source camera.
    /// </summary>
    const double FOV_V = (50 / 180) * Math.PI;
    /// <summary>
    /// The angle at which the topmost sample in the depth map starts at.
    /// </summary>
    const double V_START_ANGLE = (Math.PI - FOV_V) / 2;

    /// <summary>
    /// Builds a point cloud by projecting the supplied depth map into world space based on the source camera's configuration.
    /// </summary>
    public Matrix<double> Build(Matrix<double> depthMap)
    {
      var pointCloud = Matrix<double>.Build.Dense(2, depthMap.RowCount * depthMap.ColumnCount);

      foreach (var (y, x, depth) in depthMap.EnumerateIndexed())
      {
        // The angle the point is at w.r.t the camera plane.
        var pointHAngle = H_START_ANGLE + ((double)x / depthMap.ColumnCount) * FOV_H;
        var pointVAngle = V_START_ANGLE + ((double)y / depthMap.RowCount) * FOV_V;

        // The coordinates the point is at relative to the source camera.
        var projectedX = depth / Math.Tan(pointHAngle);
        var projectedY = depth / Math.Tan(pointVAngle);

        // TODO: Determine whether the supplied depth maps show the straight line distance from the camera or the distance perpendicular to the camera plane.
        // relativeX = relativeDepth * (pointHAngle > MathF.PI / 2 ? MathF.Cos(MathF.PI - pointHAngle) : -MathF.Cos(pointHAngle));
        // relativeY = relativeDepth * MathF.Cos(pointVAngle);
        // relativeDepth = relativeDepth * MathF.Sin(pointHAngle);

        pointCloud[0, y * depthMap.ColumnCount + x] = projectedX;
        pointCloud[1, y * depthMap.ColumnCount + x] = projectedY;
      }

      return pointCloud;
    }
  }
}