using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MailBee.Html
{
	// Token: 0x02000008 RID: 8
	public class Processor
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000052BF File Offset: 0x000042BF
		public Processor()
		{
			this.b = 0;
			this.c = true;
			this.a = new Element();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000052E0 File Offset: 0x000042E0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000052E8 File Offset: 0x000042E8
		public bool ThrowExceptions
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000052F1 File Offset: 0x000042F1
		public int LastResult
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000052FC File Offset: 0x000042FC
		public ElementReadOnlyCollection AHRefs
		{
			get
			{
				ElementCollection elementCollection = new ElementCollection();
				elementCollection.Add(this.Dom);
				this.Dom.a(elementCollection);
				for (int i = elementCollection.Count - 1; i >= 0; i--)
				{
					if (elementCollection[i].TagName != null && elementCollection[i].TagName.ToLower() == "a")
					{
						bool flag = false;
						using (IEnumerator enumerator = elementCollection[i].Attributes.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((TagAttribute)enumerator.Current).Name.ToLower() == "href")
								{
									flag = true;
								}
							}
						}
						if (!flag)
						{
							elementCollection.IsInnerTreeProcessLocked = true;
							elementCollection.Remove(elementCollection[i]);
							elementCollection.IsInnerTreeProcessLocked = false;
						}
					}
					else
					{
						elementCollection.IsInnerTreeProcessLocked = true;
						elementCollection.Remove(elementCollection[i]);
						elementCollection.IsInnerTreeProcessLocked = false;
					}
				}
				return new ElementReadOnlyCollection(elementCollection);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00005418 File Offset: 0x00004418
		public ElementReadOnlyCollection Images
		{
			get
			{
				ElementCollection elementCollection = new ElementCollection();
				elementCollection.Add(this.Dom);
				this.Dom.a(elementCollection);
				for (int i = elementCollection.Count - 1; i >= 0; i--)
				{
					if (elementCollection[i].TagName == null || elementCollection[i].TagName.ToLower() != "img")
					{
						elementCollection.IsInnerTreeProcessLocked = true;
						elementCollection.Remove(elementCollection[i]);
						elementCollection.IsInnerTreeProcessLocked = false;
					}
				}
				return new ElementReadOnlyCollection(elementCollection);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000054A3 File Offset: 0x000044A3
		public Element Dom
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000054AC File Offset: 0x000044AC
		public Task<bool> LoadFromStreamAsync(Stream inputStream, Encoding enc)
		{
			Processor.b b;
			b.c = this;
			b.d = inputStream;
			b.e = enc;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<Processor.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005504 File Offset: 0x00004504
		public Task<bool> SaveToStreamAsync(Stream outputStream, Encoding enc)
		{
			Processor.a a;
			a.c = this;
			a.d = outputStream;
			a.e = enc;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<Processor.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000555C File Offset: 0x0000455C
		public bool LoadFromStream(Stream inputStream, Encoding enc)
		{
			this.b = 0;
			if (inputStream == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			if (enc == null)
			{
				enc = Encoding.UTF8;
			}
			if (inputStream.CanRead)
			{
				try
				{
					StreamReader streamReader = new StreamReader(inputStream, enc);
					this.a = new Element(streamReader.ReadToEnd());
				}
				catch (IOException a_)
				{
					this.b = 30;
					if (this.c)
					{
						throw new MailBeeStreamException(30, a_);
					}
					return false;
				}
				return true;
			}
			this.b = 40;
			if (this.c)
			{
				throw new MailBeeStreamException(this.b);
			}
			return false;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005604 File Offset: 0x00004604
		public bool SaveToStream(Stream outputStream, Encoding enc)
		{
			this.b = 0;
			if (outputStream == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			if (enc == null)
			{
				enc = Encoding.UTF8;
			}
			try
			{
				byte[] bytes = enc.GetBytes(this.Dom.OuterHtml);
				outputStream.Write(bytes, 0, bytes.Length);
			}
			catch (IOException a_)
			{
				this.b = 30;
				if (this.c)
				{
					throw new MailBeeStreamException(30, a_);
				}
				return false;
			}
			return true;
		}

		// Token: 0x04000033 RID: 51
		private Element a;

		// Token: 0x04000034 RID: 52
		private int b;

		// Token: 0x04000035 RID: 53
		private bool c;
	}
}
