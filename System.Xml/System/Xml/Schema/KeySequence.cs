using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000191 RID: 401
	internal class KeySequence
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x0005E683 File Offset: 0x0005D683
		internal KeySequence(int dim, int line, int col)
		{
			this.dim = dim;
			this.ks = new TypedObject[dim];
			this.posline = line;
			this.poscol = col;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0005E6B3 File Offset: 0x0005D6B3
		public int PosLine
		{
			get
			{
				return this.posline;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0005E6BB File Offset: 0x0005D6BB
		public int PosCol
		{
			get
			{
				return this.poscol;
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0005E6C4 File Offset: 0x0005D6C4
		public KeySequence(TypedObject[] ks)
		{
			this.ks = ks;
			this.dim = ks.Length;
			this.posline = (this.poscol = 0);
		}

		// Token: 0x17000513 RID: 1299
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

		// Token: 0x06001534 RID: 5428 RVA: 0x0005E728 File Offset: 0x0005D728
		internal bool IsQualified()
		{
			foreach (TypedObject typedObject in this.ks)
			{
				if (typedObject == null || typedObject.Value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005E760 File Offset: 0x0005D760
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

		// Token: 0x06001536 RID: 5430 RVA: 0x0005E8D0 File Offset: 0x0005D8D0
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

		// Token: 0x06001537 RID: 5431 RVA: 0x0005E914 File Offset: 0x0005D914
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

		// Token: 0x04000CB5 RID: 3253
		private TypedObject[] ks;

		// Token: 0x04000CB6 RID: 3254
		private int dim;

		// Token: 0x04000CB7 RID: 3255
		private int hashcode = -1;

		// Token: 0x04000CB8 RID: 3256
		private int posline;

		// Token: 0x04000CB9 RID: 3257
		private int poscol;
	}
}
