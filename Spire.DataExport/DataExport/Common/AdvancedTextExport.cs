using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Common
{
	// Token: 0x0200015F RID: 351
	public abstract class AdvancedTextExport : TextExport
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x00058A78 File Offset: 0x00057A78
		protected override void SaveProperties(XMLFile File)
		{
			int a_ = 5;
			for (;;)
			{
				base.SaveProperties(File);
				File.RemoveSection(HyperlinksCollectionEditor.b("椠昢搤挦氨礪", a_));
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_140;
					case 1:
						goto IL_60;
					case 2:
						if (num >= this.Header.Count)
						{
							num2 = 6;
							continue;
						}
						File.WriteValue(HyperlinksCollectionEditor.b("椠昢搤挦氨礪", a_), string.Format(HyperlinksCollectionEditor.b("娠ጢ堤尦ᠨ嘪", a_), HyperlinksCollectionEditor.b("䴠䨢䬤䈦", a_), num), this.Header[num]);
						num++;
						num2 = 5;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						default:
						{
							if (false)
							{
							}
							int num3;
							if (num3 >= this.Footer.Count)
							{
								num2 = 4;
								continue;
							}
							File.WriteValue(HyperlinksCollectionEditor.b("朠氢樤猦氨礪", a_), string.Format(HyperlinksCollectionEditor.b("娠ጢ堤尦ᠨ嘪", a_), HyperlinksCollectionEditor.b("䴠䨢䬤䈦", a_), num3), this.Footer[num3]);
							num3++;
							if (true)
							{
							}
							num2 = 7;
							continue;
						}
						}
						break;
					case 4:
						return;
					case 5:
						goto IL_185;
					case 6:
					{
						File.RemoveSection(HyperlinksCollectionEditor.b("朠氢樤猦氨礪", a_));
						int num3 = 0;
						num2 = 0;
						continue;
					}
					case 7:
						goto IL_140;
					}
					break;
					IL_140:
					num2 = 3;
					continue;
					IL_185:
					num2 = 2;
					continue;
					IL_60:
					goto IL_185;
				}
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00058C38 File Offset: 0x00057C38
		protected override void LoadProperties(XMLFile File)
		{
			int a_ = 9;
			for (;;)
			{
				base.LoadProperties(File);
				Array array = null;
				this.Header.Clear();
				File.ReadValues(HyperlinksCollectionEditor.b("洤戦栨漪栬紮", a_), ref array);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 >= this.Header.Count)
						{
							goto IL_205;
						}
						this.Header[num2] = File.ReadValue(HyperlinksCollectionEditor.b("洤戦栨漪栬紮", a_), this.Header[num2], string.Empty);
						num2++;
						num = 10;
						continue;
					}
					case 1:
					{
						this.Header.SetStrings(array as string[]);
						int num2 = 0;
						num = 9;
						continue;
					}
					case 2:
						goto IL_B0;
					case 3:
						goto IL_15B;
					case 4:
					{
						int num3;
						if (num3 >= this.Footer.Count)
						{
							num = 11;
							continue;
						}
						this.Footer[num3] = File.ReadValue(HyperlinksCollectionEditor.b("挤栦昨缪栬紮", a_), this.Footer[num3], string.Empty);
						num3++;
						num = 3;
						continue;
					}
					case 5:
						if (array != null)
						{
							num = 1;
							continue;
						}
						goto IL_B0;
					case 6:
						goto IL_15B;
					case 7:
						if (array != null)
						{
							num = 8;
							continue;
						}
						return;
					case 8:
					{
						this.Footer.SetStrings(array as string[]);
						int num3 = 0;
						num = 6;
						continue;
					}
					case 9:
						goto IL_1E9;
					case 10:
						goto IL_1E9;
					case 11:
						return;
					}
					break;
					IL_B0:
					if (true)
					{
					}
					this.Footer.Clear();
					File.ReadValues(HyperlinksCollectionEditor.b("挤栦昨缪栬紮", a_), ref array);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_205:
						num = 2;
						continue;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					IL_15B:
					num = 4;
					continue;
					IL_1E9:
					num = 0;
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00058E5C File Offset: 0x00057E5C
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x00058EA0 File Offset: 0x00057EA0
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public new StringListCollection Header
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
				return base.Header;
			}
			set
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
				base.Header = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x00058EE4 File Offset: 0x00057EE4
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x00058F28 File Offset: 0x00057F28
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection Footer
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
				return base.Footer;
			}
			set
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
				base.Footer = value;
			}
		}
	}
}
