using System;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x020002DE RID: 734
	internal class db
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x00072F1A File Offset: 0x00071F1A
		public db()
		{
			this.a = new string[0];
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00072F30 File Offset: 0x00071F30
		public db(string[] A_0)
		{
			if (A_0 == null)
			{
				this.a = new string[0];
				return;
			}
			this.a = new string[A_0.Length];
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] == null || A_0[i].Length == 0)
				{
					throw new ArgumentException("components cannot contain null or empty strings");
				}
				this.a[i] = A_0[i];
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00072F94 File Offset: 0x00071F94
		public db(db A_0, string[] A_1)
		{
			if (A_1 == null)
			{
				this.a = new string[A_0.a.Length];
			}
			else
			{
				this.a = new string[A_0.a.Length + A_1.Length];
			}
			for (int i = 0; i < A_0.a.Length; i++)
			{
				this.a[i] = A_0.a[i];
			}
			if (A_1 != null)
			{
				for (int j = 0; j < A_1.Length; j++)
				{
					if (A_1[j] == null)
					{
						throw new ArgumentException("components cannot contain null");
					}
					int length = A_1[j].Length;
					this.a[j + A_0.a.Length] = A_1[j];
				}
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00073038 File Offset: 0x00072038
		public override bool Equals(object o)
		{
			bool result = false;
			if (o != null && o.GetType() == base.GetType())
			{
				if (this == o)
				{
					result = true;
				}
				else
				{
					db db = (db)o;
					if (db.a.Length == this.a.Length)
					{
						result = true;
						for (int i = 0; i < this.a.Length; i++)
						{
							if (!db.a[i].Equals(this.a[i]))
							{
								result = false;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000730AF File Offset: 0x000720AF
		public virtual string a(int A_0)
		{
			return this.a[A_0];
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000730BC File Offset: 0x000720BC
		public override int GetHashCode()
		{
			if (this.b == 0)
			{
				for (int i = 0; i < this.a.Length; i++)
				{
					this.b += this.a[i].GetHashCode();
				}
			}
			return this.b;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00073104 File Offset: 0x00072104
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.b();
			stringBuilder.Append(Path.DirectorySeparatorChar);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(this.a(i));
				if (i < num - 1)
				{
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0007315C File Offset: 0x0007215C
		public virtual int b()
		{
			return this.a.Length;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00073168 File Offset: 0x00072168
		public virtual db a()
		{
			int num = this.a.Length - 1;
			if (num < 0)
			{
				return null;
			}
			db db = new db(null);
			db.a = new string[num];
			Array.Copy(this.a, 0, db.a, 0, num);
			return db;
		}

		// Token: 0x0400129F RID: 4767
		private string[] a;

		// Token: 0x040012A0 RID: 4768
		private int b;
	}
}
