using System;

namespace DBot.Perception
{
  public class PointCloudBuilder
  {
    /// <summary>
    /// The number of pixel steps to take for each sample of the depth map.
    /// </summary>
    const int DEPTH_MAP_STRIDE_LENGTH = 1;

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

    public void Build()
    {

    }
  }
}