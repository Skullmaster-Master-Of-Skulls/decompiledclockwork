using System;
using System.Collections;
using System.Reflection;

namespace a.g
{
	// Token: 0x02000403 RID: 1027
	[DefaultMember("Item")]
	internal class d : CollectionBase
	{
		// Token: 0x0600241E RID: 9246 RVA: 0x00099E52 File Offset: 0x00098E52
		public f a(int A_0)
		{
			return (f)base.List[A_0];
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00099E65 File Offset: 0x00098E65
		public void b(int A_0, f A_1)
		{
			base.List[A_0] = A_1;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00099E74 File Offset: 0x00098E74
		public void a(f A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00099E83 File Offset: 0x00098E83
		public void a(int A_0, f A_1)
		{
			base.List.Insert(A_0, A_1);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00099E92 File Offset: 0x00098E92
		public void b(int A_0)
		{
			base.List.RemoveAt(A_0);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00099EA0 File Offset: 0x00098EA0
		public void a()
		{
			base.List.Clear();
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00099EAD File Offset: 0x00098EAD
		protected override void OnValidate(object value)
		{
		}
	}
}
