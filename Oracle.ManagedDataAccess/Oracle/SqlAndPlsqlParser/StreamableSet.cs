using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000287 RID: 647
	internal class StreamableSet<T> : Set<T>, IStreamable where T : IStreamable, new()
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x0010884C File Offset: 0x00106A4C
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

		// Token: 0x06001947 RID: 6471 RVA: 0x001088CC File Offset: 0x00106ACC
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

		// Token: 0x06001948 RID: 6472 RVA: 0x00108964 File Offset: 0x00106B64
		public StreamableList<T> GetList()
		{
			return new StreamableList<T>(this);
		}
	}
}
