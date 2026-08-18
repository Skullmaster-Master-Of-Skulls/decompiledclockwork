using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace a.b
{
	// Token: 0x020003B2 RID: 946
	[DefaultMember("Item")]
	internal sealed class hx : ReadOnlyCollectionBase, i9
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x0008BE70 File Offset: 0x0008AE70
		public hx()
		{
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0008BE78 File Offset: 0x0008AE78
		public hx(ICollection A_0)
		{
			base.InnerList.AddRange(A_0);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0008BE8C File Offset: 0x0008AE8C
		public hx(i9 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("collection");
			}
			base.InnerList.Capacity = A_0.get_Count();
			foreach (object obj in A_0)
			{
				string value = (string)obj;
				base.InnerList.Add(value);
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0008BF0C File Offset: 0x0008AF0C
		public hx(params string[] A_0)
		{
			if (A_0 != null)
			{
				foreach (string value in A_0)
				{
					base.InnerList.Add(value);
				}
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0008BF43 File Offset: 0x0008AF43
		public string ot(int A_0)
		{
			return base.InnerList[A_0] as string;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0008BF56 File Offset: 0x0008AF56
		public int c(string A_0)
		{
			return base.InnerList.Add(A_0);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0008BF64 File Offset: 0x0008AF64
		public void b(string A_0)
		{
			base.InnerList.Remove(A_0);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0008BF72 File Offset: 0x0008AF72
		public void a(int A_0)
		{
			base.InnerList.RemoveAt(A_0);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0008BF80 File Offset: 0x0008AF80
		public void a(string A_0, int A_1)
		{
			base.InnerList.Insert(A_1, A_0);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0008BF90 File Offset: 0x0008AF90
		public void b(i9 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in A_0)
			{
				string value = (string)obj;
				base.InnerList.Add(value);
			}
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0008BFF8 File Offset: 0x0008AFF8
		public void d(string A_0)
		{
			if (!string.IsNullOrEmpty(A_0))
			{
				string[] array = A_0.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					base.InnerList.Add(array[i].Trim());
				}
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0008C041 File Offset: 0x0008B041
		public static hx a(string A_0)
		{
			hx hx = new hx();
			hx.d(A_0);
			return hx;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0008C050 File Offset: 0x0008B050
		public string ou()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object obj in base.InnerList)
			{
				string value = (string)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0008C0D0 File Offset: 0x0008B0D0
		public void a(i9 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in A_0)
			{
				string a_ = (string)obj;
				this.b(a_);
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0008C134 File Offset: 0x0008B134
		public void b()
		{
			base.InnerList.Clear();
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0008C144 File Offset: 0x0008B144
		public void a()
		{
			int count = base.InnerList.Count;
			string[] array = new string[count];
			base.InnerList.CopyTo(array);
			Array.Sort<string>(array);
			for (int i = 0; i < count; i++)
			{
				base.InnerList[i] = array[i];
			}
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0008C191 File Offset: 0x0008B191
		public void ov(string[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0008C1A0 File Offset: 0x0008B1A0
		public int ow(string A_0)
		{
			int count = base.InnerList.Count;
			for (int i = 0; i < count; i++)
			{
				if (au.a(A_0, base.InnerList[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0008C1DC File Offset: 0x0008B1DC
		public bool ox(string A_0)
		{
			return this.ow(A_0) >= 0;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0008C1EB File Offset: 0x0008B1EB
		public override bool Equals(object obj)
		{
			return i2.a(this, obj);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0008C1F4 File Offset: 0x0008B1F4
		public override int GetHashCode()
		{
			return i2.b(this);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0008C1FC File Offset: 0x0008B1FC
		public override string ToString()
		{
			return i2.a(this);
		}

		// Token: 0x04001689 RID: 5769
		public static readonly i9 a = new hx();
	}
}
