using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020001E9 RID: 489
	internal class KeySequence
	{
		// Token: 0x06002079 RID: 8313 RVA: 0x000B230E File Offset: 0x000B050E
		internal KeySequence(int dim, int line, int col)
		{
			this.dim = dim;
			this.ks = new TypedObject[dim];
			this.posline = line;
			this.poscol = col;
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x000B233E File Offset: 0x000B053E
		public int PosLine
		{
			get
			{
				return this.posline;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x000B2346 File Offset: 0x000B0546
		public int PosCol
		{
			get
			{
				return this.poscol;
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000B2350 File Offset: 0x000B0550
		public KeySequence(TypedObject[] ks)
		{
			this.ks = ks;
			this.dim = ks.Length;
			this.posline = (this.poscol = 0);
		}

		// Token: 0x170006B5 RID: 1717
		public object this[int index]
		{
			get
			{
				return this.ks[index];
			}
			set
			{
				this.ks[index] = (TypedObject)value;
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000B23B4 File Offset: 0x000B05B4
		internal bool IsQualified()
		{
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (this.ks[i] == null || this.ks[i].Value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000B23F0 File Offset: 0x000B05F0
		public override int GetHashCode()
		{
			if (this.hashcode != -1)
			{
				return this.hashcode;
			}
			this.hashcode = 0;
			for (int i = 0; i < this.ks.Length; i++)
			{
				this.ks[i].SetDecimal();
				if (this.ks[i].IsDecimal)
				{
					for (int j = 0; j < this.ks[i].Dim; j++)
					{
						this.hashcode += this.ks[i].Dvalue[j].GetHashCode();
					}
				}
				else
				{
					Array array = this.ks[i].Value as Array;
					if (array != null)
					{
						XmlAtomicValue[] array2 = array as XmlAtomicValue[];
						if (array2 != null)
						{
							for (int k = 0; k < array2.Length; k++)
							{
								this.hashcode += ((XmlAtomicValue)array2.GetValue(k)).TypedValue.GetHashCode();
							}
						}
						else
						{
							for (int l = 0; l < ((Array)this.ks[i].Value).Length; l++)
							{
								this.hashcode += ((Array)this.ks[i].Value).GetValue(l).GetHashCode();
							}
						}
					}
					else
					{
						this.hashcode += this.ks[i].Value.GetHashCode();
					}
				}
			}
			return this.hashcode;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000B2560 File Offset: 0x000B0760
		public override bool Equals(object other)
		{
			KeySequence keySequence = (KeySequence)other;
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (!this.ks[i].Equals(keySequence.ks[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000B25A4 File Offset: 0x000B07A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ks[0].ToString());
			for (int i = 1; i < this.ks.Length; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this.ks[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000DAA RID: 3498
		private TypedObject[] ks;

		// Token: 0x04000DAB RID: 3499
		private int dim;

		// Token: 0x04000DAC RID: 3500
		private int hashcode = -1;

		// Token: 0x04000DAD RID: 3501
		private int posline;

		// Token: 0x04000DAE RID: 3502
		private int poscol;
	}
}
