using System;
using System.Collections;

namespace MailBee.Mime
{
	// Token: 0x02000566 RID: 1382
	public class MimePartCollection : CollectionBase
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000DD54C File Offset: 0x000DC54C
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x000DD5B4 File Offset: 0x000DC5B4
		internal bool NeedToRebuild
		{
			get
			{
				using (IEnumerator enumerator = base.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((MimePart)enumerator.Current).NeedToRebuild)
						{
							this.a = true;
							break;
						}
					}
				}
				return this.a;
			}
			set
			{
				foreach (object obj in base.List)
				{
					((MimePart)obj).NeedToRebuild = value;
				}
				this.a = value;
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000DD614 File Offset: 0x000DC614
		internal MimePartCollection()
		{
		}

		// Token: 0x17000589 RID: 1417
		public MimePart this[int index]
		{
			get
			{
				return (MimePart)base.List[index];
			}
		}

		// Token: 0x1700058A RID: 1418
		public MimePart this[string name]
		{
			get
			{
				foreach (object obj in base.List)
				{
					MimePart mimePart = (MimePart)obj;
					if (mimePart.ContentTypeHeader != null && mimePart.ContentTypeHeader.Value != null && string.Compare(mimePart.ContentTypeHeader.Value, name, true) == 0)
					{
						return mimePart;
					}
				}
				return null;
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000DD6B4 File Offset: 0x000DC6B4
		internal int b(MimePart A_0)
		{
			this.a = true;
			return base.List.Add(A_0);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000DD6C9 File Offset: 0x000DC6C9
		internal int d(MimePart A_0)
		{
			return base.List.IndexOf(A_0);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000DD6D7 File Offset: 0x000DC6D7
		internal void a(int A_0, MimePart A_1)
		{
			base.List.Insert(A_0, A_1);
			this.a = true;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000DD6ED File Offset: 0x000DC6ED
		internal void a(MimePart A_0)
		{
			base.List.Remove(A_0);
			this.a = true;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000DD702 File Offset: 0x000DC702
		internal void b()
		{
			base.List.Clear();
			this.a = true;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000DD716 File Offset: 0x000DC716
		internal void a(int A_0)
		{
			base.List.RemoveAt(A_0);
			this.a = true;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000DD72B File Offset: 0x000DC72B
		internal bool c(MimePart A_0)
		{
			return base.List.Contains(A_0);
		}

		// Token: 0x04001FA1 RID: 8097
		private bool a;
	}
}
