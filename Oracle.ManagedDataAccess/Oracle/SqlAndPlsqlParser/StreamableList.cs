using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000288 RID: 648
	internal class StreamableList<T> : List<T>, IStreamable where T : IStreamable, new()
	{
		// Token: 0x0600194A RID: 6474 RVA: 0x00108974 File Offset: 0x00106B74
		public StreamableList(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00108980 File Offset: 0x00106B80
		public StreamableList()
		{
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00108988 File Offset: 0x00106B88
		public int ReadFromStream(InputStream istrm)
		{
			if (istrm.ReadLine() == base.GetType().Name)
			{
				istrm.ReadLine();
				int num = int.Parse(istrm.ReadLine());
				for (int i = 0; i < num; i++)
				{
					T item = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
					item.ReadFromStream(istrm);
					base.Add(item);
				}
				return 0;
			}
			return -1;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00108A08 File Offset: 0x00106C08
		public int WriteToStream(OutputStream ostrm)
		{
			ostrm.WriteLine(base.GetType().Name);
			ostrm.WriteLine(typeof(T).Name);
			ostrm.WriteLine(base.Count.ToString());
			foreach (T t in this)
			{
				t.WriteToStream(ostrm);
			}
			ostrm.Flush();
			return 0;
		}
	}
}
