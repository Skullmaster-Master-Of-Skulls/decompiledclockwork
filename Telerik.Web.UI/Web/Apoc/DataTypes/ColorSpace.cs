using System;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200137A RID: 4986
	internal class ColorSpace
	{
		// Token: 0x0600D00C RID: 53260 RVA: 0x002E14F0 File Offset: 0x002DF6F0
		public ColorSpace(int theColorSpace)
		{
			this.currentColorSpace = theColorSpace;
			this._hasICCProfile = false;
			this.numComponents = this.CalculateNumComponents();
		}

		// Token: 0x0600D00D RID: 53261 RVA: 0x002E1519 File Offset: 0x002DF719
		private int CalculateNumComponents()
		{
			if (this.currentColorSpace == 1)
			{
				return 1;
			}
			if (this.currentColorSpace == 2)
			{
				return 3;
			}
			if (this.currentColorSpace == 3)
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x0600D00E RID: 53262 RVA: 0x002E153D File Offset: 0x002DF73D
		public void SetColorSpace(int theColorSpace)
		{
			this.currentColorSpace = theColorSpace;
			this.numComponents = this.CalculateNumComponents();
		}

		// Token: 0x0600D00F RID: 53263 RVA: 0x002E1552 File Offset: 0x002DF752
		public bool HasICCProfile()
		{
			return this._hasICCProfile;
		}

		// Token: 0x0600D010 RID: 53264 RVA: 0x002E155A File Offset: 0x002DF75A
		public byte[] GetICCProfile()
		{
			if (this._hasICCProfile)
			{
				return this.iccProfile;
			}
			return new byte[0];
		}

		// Token: 0x0600D011 RID: 53265 RVA: 0x002E1571 File Offset: 0x002DF771
		public void SetICCProfile(byte[] iccProfile)
		{
			this.iccProfile = iccProfile;
			this._hasICCProfile = true;
		}

		// Token: 0x0600D012 RID: 53266 RVA: 0x002E1581 File Offset: 0x002DF781
		public int GetColorSpace()
		{
			return this.currentColorSpace;
		}

		// Token: 0x0600D013 RID: 53267 RVA: 0x002E1589 File Offset: 0x002DF789
		public int GetNumComponents()
		{
			return this.numComponents;
		}

		// Token: 0x0600D014 RID: 53268 RVA: 0x002E1591 File Offset: 0x002DF791
		public string GetColorSpacePDFString()
		{
			if (this.currentColorSpace == 2)
			{
				return "DeviceRGB";
			}
			if (this.currentColorSpace == 3)
			{
				return "DeviceCMYK";
			}
			if (this.currentColorSpace == 1)
			{
				return "DeviceGray";
			}
			return "DeviceRGB";
		}

		// Token: 0x040037C1 RID: 14273
		public const int DEVICE_UNKNOWN = -1;

		// Token: 0x040037C2 RID: 14274
		public const int DEVICE_GRAY = 1;

		// Token: 0x040037C3 RID: 14275
		public const int DEVICE_RGB = 2;

		// Token: 0x040037C4 RID: 14276
		public const int DEVICE_CMYK = 3;

		// Token: 0x040037C5 RID: 14277
		private bool _hasICCProfile;

		// Token: 0x040037C6 RID: 14278
		private byte[] iccProfile;

		// Token: 0x040037C7 RID: 14279
		private int numComponents;

		// Token: 0x040037C8 RID: 14280
		protected int currentColorSpace = -1;
	}
}
