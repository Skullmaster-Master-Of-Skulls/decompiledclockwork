using System;

namespace System.Web.Helpers
{
	// Token: 0x02000008 RID: 8
	public static class ChartTheme
	{
		// Token: 0x04000025 RID: 37
		public const string Blue = "<Chart BackColor=\"#D3DFF0\" BackGradientStyle=\"TopBottom\" BackSecondaryColor=\"White\" BorderColor=\"26, 59, 105\" BorderlineDashStyle=\"Solid\" BorderWidth=\"2\" Palette=\"BrightPastel\">\r\n    <ChartAreas>\r\n        <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"64, 165, 191, 228\" BackGradientStyle=\"TopBottom\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\" /> \r\n    </ChartAreas>\r\n    <Legends>\r\n        <Legend _Template_=\"All\" BackColor=\"Transparent\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit=\"False\" /> \r\n    </Legends>\r\n    <BorderSkin SkinStyle=\"Emboss\" /> \r\n  </Chart>";

		// Token: 0x04000026 RID: 38
		public const string Green = "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>";

		// Token: 0x04000027 RID: 39
		public const string Vanilla = "<Chart Palette=\"SemiTransparent\" BorderColor=\"#000\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\">\r\n<ChartAreas>\r\n    <ChartArea _Template_=\"All\" Name=\"Default\">\r\n            <AxisX>\r\n                <MinorGrid Enabled=\"False\" />\r\n                <MajorGrid Enabled=\"False\" />\r\n            </AxisX>\r\n            <AxisY>\r\n                <MajorGrid Enabled=\"False\" />\r\n                <MinorGrid Enabled=\"False\" />\r\n            </AxisY>\r\n    </ChartArea>\r\n</ChartAreas>\r\n</Chart>";

		// Token: 0x04000028 RID: 40
		public const string Vanilla3D = "<Chart BackColor=\"#555\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"SemiTransparent\" AntiAliasing=\"All\">\r\n    <ChartAreas>\r\n        <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n            <Area3DStyle LightStyle=\"Simplistic\" Enable3D=\"True\" Inclination=\"30\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"-30\" WallWidth=\"0\" />\r\n        </ChartArea>\r\n    </ChartAreas>\r\n</Chart>";

		// Token: 0x04000029 RID: 41
		public const string Yellow = "<Chart BackColor=\"#FADA5E\" BackGradientStyle=\"TopBottom\" BorderColor=\"#B8860B\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"EarthTones\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY>\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>";
	}
}
