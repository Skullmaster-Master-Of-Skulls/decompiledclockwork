using System;

namespace Telerik.Web.Apoc.Image
{
	// Token: 0x020015D4 RID: 5588
	internal class JpegInfo
	{
		// Token: 0x0600D9F8 RID: 55800 RVA: 0x002FC84C File Offset: 0x002FAA4C
		internal void SetNumColourComponents(int colourComponents)
		{
			switch (colourComponents)
			{
			case 1:
				this.colourSpace = 1;
				return;
			case 3:
				this.colourSpace = 2;
				return;
			case 4:
				this.colourSpace = 3;
				return;
			}
			this.colourSpace = -1;
		}

		// Token: 0x0600D9F9 RID: 55801 RVA: 0x002FC894 File Offset: 0x002FAA94
		internal void SetBitsPerSample(int bitsPerSample)
		{
			this.bitsPerSample = bitsPerSample;
		}

		// Token: 0x0600D9FA RID: 55802 RVA: 0x002FC89D File Offset: 0x002FAA9D
		internal void SetWidth(int width)
		{
			this.width = width;
		}

		// Token: 0x0600D9FB RID: 55803 RVA: 0x002FC8A6 File Offset: 0x002FAAA6
		internal void SetHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x0600D9FC RID: 55804 RVA: 0x002FC8AF File Offset: 0x002FAAAF
		internal void SetICCProfile(byte[] profileData)
		{
			this.profileData = profileData;
		}

		// Token: 0x17004302 RID: 17154
		// (get) Token: 0x0600D9FD RID: 55805 RVA: 0x002FC8B8 File Offset: 0x002FAAB8
		public byte[] ICCProfileData
		{
			get
			{
				return this.profileData;
			}
		}

		// Token: 0x17004303 RID: 17155
		// (get) Token: 0x0600D9FE RID: 55806 RVA: 0x002FC8C0 File Offset: 0x002FAAC0
		public bool HasICCProfile
		{
			get
			{
				return this.profileData != null;
			}
		}

		// Token: 0x17004304 RID: 17156
		// (get) Token: 0x0600D9FF RID: 55807 RVA: 0x002FC8CE File Offset: 0x002FAACE
		public int ColourSpace
		{
			get
			{
				return this.colourSpace;
			}
		}

		// Token: 0x17004305 RID: 17157
		// (get) Token: 0x0600DA00 RID: 55808 RVA: 0x002FC8D6 File Offset: 0x002FAAD6
		public int BitsPerSample
		{
			get
			{
				return this.bitsPerSample;
			}
		}

		// Token: 0x17004306 RID: 17158
		// (get) Token: 0x0600DA01 RID: 55809 RVA: 0x002FC8DE File Offset: 0x002FAADE
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17004307 RID: 17159
		// (get) Token: 0x0600DA02 RID: 55810 RVA: 0x002FC8E6 File Offset: 0x002FAAE6
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x04003C6D RID: 15469
		private int colourSpace = -1;

		// Token: 0x04003C6E RID: 15470
		private int bitsPerSample;

		// Token: 0x04003C6F RID: 15471
		private int width;

		// Token: 0x04003C70 RID: 15472
		private int height;

		// Token: 0x04003C71 RID: 15473
		private byte[] profileData;
	}
}
