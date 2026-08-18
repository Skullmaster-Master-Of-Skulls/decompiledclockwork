using System;
using System.Collections;
using System.Data;

namespace MailBee.SmtpMail
{
	// Token: 0x0200013F RID: 319
	public class SendMailJobCollection : ICollection
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x0002EFD5 File Offset: 0x0002DFD5
		internal SendMailJobCollection(bool A_0, object A_1)
		{
			this.a = null;
			this.b = A_0;
			this.c = A_1;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002EFF2 File Offset: 0x0002DFF2
		private ArrayList InnerList
		{
			get
			{
				if (this.a == null)
				{
					this.a = new ArrayList();
				}
				return this.a;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002F00D File Offset: 0x0002E00D
		public int Count
		{
			get
			{
				if (this.a != null)
				{
					return this.a.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002F024 File Offset: 0x0002E024
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002F027 File Offset: 0x0002E027
		public object SyncRoot
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002F02F File Offset: 0x0002E02F
		public void CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002F03E File Offset: 0x0002E03E
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x17000330 RID: 816
		public SendMailJob this[int index]
		{
			get
			{
				return (SendMailJob)this.InnerList[index];
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002F05E File Offset: 0x0002E05E
		internal void a(SendMailJob A_0)
		{
			this.InnerList.Add(A_0);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002F06D File Offset: 0x0002E06D
		public void Add(SendMailJob job)
		{
			if (this.b)
			{
				throw new MailBeeInvalidStateException(12);
			}
			if (job == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.a(job);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002F091 File Offset: 0x0002E091
		internal void c(SendMailJob A_0)
		{
			this.InnerList.Insert(0, A_0);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002F0A0 File Offset: 0x0002E0A0
		internal void b(SendMailJob A_0)
		{
			this.InnerList.Remove(A_0);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002F0AE File Offset: 0x0002E0AE
		public void Remove(SendMailJob job)
		{
			if (this.b)
			{
				throw new MailBeeInvalidStateException(12);
			}
			this.b(job);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002F0C7 File Offset: 0x0002E0C7
		internal void a(int A_0)
		{
			this.InnerList.RemoveAt(A_0);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002F0D5 File Offset: 0x0002E0D5
		public void RemoveAt(int index)
		{
			if (this.b)
			{
				throw new MailBeeInvalidStateException(12);
			}
			this.a(index);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002F0EE File Offset: 0x0002E0EE
		internal void b()
		{
			this.InnerList.Clear();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002F0FB File Offset: 0x0002E0FB
		public void Clear()
		{
			if (this.b)
			{
				throw new MailBeeInvalidStateException(12);
			}
			this.b();
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002F113 File Offset: 0x0002E113
		public bool IsReadOnly
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002F11C File Offset: 0x0002E11C
		public int[] GetIndicesAsArray(DataTable mergeTable, string tag)
		{
			if (this.a == null)
			{
				return new int[0];
			}
			int num = 0;
			for (int i = 0; i < this.a.Count; i++)
			{
				if ((mergeTable == null || mergeTable == ((SendMailJob)this.a[i]).MergeTable) && (tag == null || tag == ((SendMailJob)this.a[i]).Tag))
				{
					num += ((SendMailJob)this.a[i]).a(null, 0);
				}
			}
			int[] array = new int[num];
			int j = 0;
			int num2 = 0;
			while (j < this.a.Count)
			{
				if ((mergeTable == null || mergeTable == ((SendMailJob)this.a[j]).MergeTable) && (tag == null || tag == ((SendMailJob)this.a[j]).Tag))
				{
					num2 += ((SendMailJob)this.a[j]).a(array, num2);
				}
				j++;
			}
			return array;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002F224 File Offset: 0x0002E224
		public string GetIndicesAsString(DataTable mergeTable, string tag)
		{
			if (this.a == null)
			{
				return string.Empty;
			}
			int num;
			if (mergeTable == null && tag == null)
			{
				num = this.a.Count;
			}
			else
			{
				num = 0;
				for (int i = 0; i < this.a.Count; i++)
				{
					if ((mergeTable == null || mergeTable == ((SendMailJob)this.a[i]).MergeTable) && (tag == null || tag == ((SendMailJob)this.a[i]).Tag))
					{
						num++;
					}
				}
			}
			string[] array = new string[num];
			int j = 0;
			int num2 = 0;
			while (j < this.a.Count)
			{
				if ((mergeTable == null || mergeTable == ((SendMailJob)this.a[j]).MergeTable) && (tag == null || tag == ((SendMailJob)this.a[j]).Tag))
				{
					array[num2] = ((SendMailJob)this.a[j]).GetIndicesAsString();
					num2++;
				}
				j++;
			}
			return string.Join(",", array);
		}

		// Token: 0x04000806 RID: 2054
		private ArrayList a;

		// Token: 0x04000807 RID: 2055
		private bool b;

		// Token: 0x04000808 RID: 2056
		private object c;
	}
}
