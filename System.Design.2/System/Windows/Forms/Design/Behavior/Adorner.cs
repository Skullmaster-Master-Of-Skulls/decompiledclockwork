using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200036F RID: 879
	public sealed class Adorner
	{
		// Token: 0x060023F6 RID: 9206 RVA: 0x000E04E9 File Offset: 0x000DE6E9
		public Adorner()
		{
			this.glyphs = new GlyphCollection();
			this.enabled = true;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000E0503 File Offset: 0x000DE703
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x000E050B File Offset: 0x000DE70B
		public BehaviorService BehaviorService
		{
			get
			{
				return this.behaviorService;
			}
			set
			{
				this.behaviorService = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000E0514 File Offset: 0x000DE714
		// (set) Token: 0x060023FA RID: 9210 RVA: 0x000E051C File Offset: 0x000DE71C
		public bool Enabled
		{
			get
			{
				return this.EnabledInternal;
			}
			set
			{
				if (value != this.EnabledInternal)
				{
					this.EnabledInternal = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000E0534 File Offset: 0x000DE734
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000E053C File Offset: 0x000DE73C
		internal bool EnabledInternal
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000E0545 File Offset: 0x000DE745
		public GlyphCollection Glyphs
		{
			get
			{
				return this.glyphs;
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000E054D File Offset: 0x000DE74D
		public void Invalidate()
		{
			if (this.behaviorService != null)
			{
				this.behaviorService.Invalidate();
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000E0562 File Offset: 0x000DE762
		public void Invalidate(Rectangle rectangle)
		{
			if (this.behaviorService != null)
			{
				this.behaviorService.Invalidate(rectangle);
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000E0578 File Offset: 0x000DE778
		public void Invalidate(Region region)
		{
			if (this.behaviorService != null)
			{
				this.behaviorService.Invalidate(region);
			}
		}

		// Token: 0x04001A48 RID: 6728
		private BehaviorService behaviorService;

		// Token: 0x04001A49 RID: 6729
		private GlyphCollection glyphs;

		// Token: 0x04001A4A RID: 6730
		private bool enabled;
	}
}
