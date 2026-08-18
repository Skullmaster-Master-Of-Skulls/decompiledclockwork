using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001E RID: 30
	internal class AttributesCriterion : SelectionCriterion
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002B90 File Offset: 0x00000D90
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002C34 File Offset: 0x00000E34
		internal string AttributeString
		{
			get
			{
				string text = "";
				if ((this._Attributes & FileAttributes.Hidden) != (FileAttributes)0)
				{
					text += "H";
				}
				if ((this._Attributes & FileAttributes.System) != (FileAttributes)0)
				{
					text += "S";
				}
				if ((this._Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
				{
					text += "R";
				}
				if ((this._Attributes & FileAttributes.Archive) != (FileAttributes)0)
				{
					text += "A";
				}
				if ((this._Attributes & FileAttributes.ReparsePoint) != (FileAttributes)0)
				{
					text += "L";
				}
				if ((this._Attributes & FileAttributes.NotContentIndexed) != (FileAttributes)0)
				{
					text += "I";
				}
				return text;
			}
			set
			{
				this._Attributes = FileAttributes.Normal;
				foreach (char c in value.ToUpper())
				{
					char c2 = c;
					if (c2 != 'A')
					{
						switch (c2)
						{
						case 'H':
							if ((this._Attributes & FileAttributes.Hidden) != (FileAttributes)0)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.Hidden;
							goto IL_1C1;
						case 'I':
							if ((this._Attributes & FileAttributes.NotContentIndexed) != (FileAttributes)0)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.NotContentIndexed;
							goto IL_1C1;
						case 'J':
						case 'K':
							break;
						case 'L':
							if ((this._Attributes & FileAttributes.ReparsePoint) != (FileAttributes)0)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.ReparsePoint;
							goto IL_1C1;
						default:
							switch (c2)
							{
							case 'R':
								if ((this._Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.ReadOnly;
								goto IL_1C1;
							case 'S':
								if ((this._Attributes & FileAttributes.System) != (FileAttributes)0)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.System;
								goto IL_1C1;
							}
							break;
						}
						throw new ArgumentException(value);
					}
					if ((this._Attributes & FileAttributes.Archive) != (FileAttributes)0)
					{
						throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
					}
					this._Attributes |= FileAttributes.Archive;
					IL_1C1:;
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E14 File Offset: 0x00001014
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("attributes ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ").Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E64 File Offset: 0x00001064
		private bool _EvaluateOne(FileAttributes fileAttrs, FileAttributes criterionAttrs)
		{
			return (this._Attributes & criterionAttrs) != criterionAttrs || (fileAttrs & criterionAttrs) == criterionAttrs;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E8C File Offset: 0x0000108C
		internal override bool Evaluate(string filename)
		{
			if (Directory.Exists(filename))
			{
				return this.Operator != ComparisonOperator.EqualTo;
			}
			FileAttributes attributes = File.GetAttributes(filename);
			return this._Evaluate(attributes);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EBC File Offset: 0x000010BC
		private bool _Evaluate(FileAttributes fileAttrs)
		{
			bool flag = this._EvaluateOne(fileAttrs, FileAttributes.Hidden);
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.System);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.ReadOnly);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.Archive);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.NotContentIndexed);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.ReparsePoint);
			}
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F28 File Offset: 0x00001128
		internal override bool Evaluate(ZipEntry entry)
		{
			FileAttributes attributes = entry.Attributes;
			return this._Evaluate(attributes);
		}

		// Token: 0x0400004C RID: 76
		private FileAttributes _Attributes;

		// Token: 0x0400004D RID: 77
		internal ComparisonOperator Operator;
	}
}
