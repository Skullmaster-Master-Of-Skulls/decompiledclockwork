using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Common;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.Access
{
	// Token: 0x020001EA RID: 490
	public abstract class DatabaseExport : ExportBase
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x000A3BD0 File Offset: 0x000A2BD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void SaveToFile()
		{
			int a_ = 17;
			switch (0)
			{
			default:
				for (;;)
				{
					base.SaveToFile();
					int num = 3;
					for (;;)
					{
						spr\u1BFE spr_u1BFE;
						switch (num)
						{
						case 0:
						{
							string startupPath;
							this.ᜀ = startupPath + '\\' + this.ᜀ;
							num = 5;
							continue;
						}
						case 1:
						{
							string startupPath = Application.StartupPath;
							num = 4;
							continue;
						}
						case 2:
							return;
						case 3:
							if (!this.m_exportIfEmpty)
							{
								num = 11;
								continue;
							}
							goto IL_1BD;
						case 4:
						{
							string startupPath;
							if (startupPath.Length > 0)
							{
								num = 0;
								continue;
							}
							goto IL_78;
						}
						case 5:
							goto IL_78;
						case 6:
							try
							{
								base.DoExport();
								return;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										((IDisposable)spr_u1BFE).Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_14D;
									}
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_14F;
									default:
										if (false)
										{
										}
										if (spr_u1BFE == null)
										{
											goto IL_14F;
										}
										num = 0;
										break;
									}
								}
								IL_14D:
								IL_14F:;
							}
							goto IL_150;
						case 7:
							goto IL_1E2;
						case 8:
							if (base.\u1733())
							{
								num = 2;
								continue;
							}
							goto IL_1BD;
						case 9:
							if (this.ᜀ.Length == 0)
							{
								num = 7;
								continue;
							}
							num = 10;
							continue;
						case 10:
							if (Path.GetDirectoryName(this.ᜀ).Length == 0)
							{
								num = 1;
								continue;
							}
							goto IL_78;
						case 11:
							goto IL_150;
						}
						break;
						IL_78:
						Type writerType = this.GetWriterType();
						object[] array = new object[2];
						array[0] = this;
						spr_u1BFE = (this.\u171C = (spr\u1BFE)Activator.CreateInstance(writerType, array));
						if (true)
						{
						}
						num = 6;
						continue;
						IL_150:
						num = 8;
						continue;
						IL_1BD:
						num = 9;
					}
				}
				return;
				IL_1E2:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氬崮嘰䀲樴猶堸伺尼崾⁀あ⁄ॆ⡈♊⡌", a_)));
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000A3E04 File Offset: 0x000A2E04
		public override void SaveToStream(Stream Stream)
		{
			int num = 0;
			for (;;)
			{
				spr\u1BFE spr_u1BFE;
				switch (num)
				{
				case 1:
					try
					{
						base.DoExport();
						goto IL_103;
					}
					finally
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_A9;
								case 2:
									((IDisposable)spr_u1BFE).Dispose();
									num = 0;
									continue;
								}
								if (spr_u1BFE == null)
								{
									break;
								}
								num = 2;
							}
							IL_A9:
							break;
						}
					}
					goto IL_AC;
				case 2:
					num = 4;
					continue;
				case 3:
					return;
				case 4:
					if (base.\u1733())
					{
						num = 3;
						continue;
					}
					goto IL_AC;
				}
				if (!this.m_exportIfEmpty)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				IL_AC:
				Type writerType = this.GetWriterType();
				object[] array = new object[2];
				array[0] = this;
				spr_u1BFE = (this.\u171C = (spr\u1BFE)Activator.CreateInstance(writerType, array));
				num = 1;
			}
			return;
			IL_103:
			base.SaveToStream(Stream);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x000A3F2C File Offset: 0x000A2F2C
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x000A3F70 File Offset: 0x000A2F70
		protected string TableName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜁ = value;
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					if (!(value != this.ᜁ))
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x000A3FF0 File Offset: 0x000A2FF0
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x000A4034 File Offset: 0x000A3034
		protected bool CreateDatabase
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜂ;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜂ = value;
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						return;
					}
					if (value == this.ᜂ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000A40B0 File Offset: 0x000A30B0
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000A40F4 File Offset: 0x000A30F4
		protected bool CreateTable
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜃ;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜃ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000A4170 File Offset: 0x000A3170
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x000A41B4 File Offset: 0x000A31B4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DatabaseName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜀ = value;
						if (true)
						{
						}
						num = 1;
						continue;
					}
					if (!(value != this.ᜀ))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x04000B4B RID: 2891
		private bool \u25D8\u0094\u009D\u008D;

		// Token: 0x04000B4C RID: 2892
		private new string ᜀ = string.Empty;

		// Token: 0x04000B4D RID: 2893
		private new string ᜁ = string.Empty;

		// Token: 0x04000B4E RID: 2894
		private bool[] \u2609\u0085\u0082\u00AC;

		// Token: 0x04000B4F RID: 2895
		private new bool ᜂ;

		// Token: 0x04000B50 RID: 2896
		private new bool ᜃ;
	}
}
