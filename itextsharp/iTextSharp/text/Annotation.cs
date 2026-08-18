using System;
using System.Collections.Generic;

namespace iTextSharp.text
{
	// Token: 0x020003C0 RID: 960
	public class Annotation : IElement
	{
		// Token: 0x06002165 RID: 8549 RVA: 0x000C9B88 File Offset: 0x000C8B88
		private Annotation(float llx, float lly, float urx, float ury)
		{
			this.llx = llx;
			this.lly = lly;
			this.urx = urx;
			this.ury = ury;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000C9BF0 File Offset: 0x000C8BF0
		public Annotation(Annotation an)
		{
			this.annotationtype = an.annotationtype;
			this.annotationAttributes = an.annotationAttributes;
			this.llx = an.llx;
			this.lly = an.lly;
			this.urx = an.urx;
			this.ury = an.ury;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000C9C84 File Offset: 0x000C8C84
		public Annotation(string title, string text)
		{
			this.annotationtype = 0;
			this.annotationAttributes["title"] = title;
			this.annotationAttributes["content"] = text;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000C9CF7 File Offset: 0x000C8CF7
		public Annotation(string title, string text, float llx, float lly, float urx, float ury) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 0;
			this.annotationAttributes["title"] = title;
			this.annotationAttributes["content"] = text;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000C9D2F File Offset: 0x000C8D2F
		public Annotation(float llx, float lly, float urx, float ury, Uri url) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 1;
			this.annotationAttributes["url"] = url;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000C9D55 File Offset: 0x000C8D55
		public Annotation(float llx, float lly, float urx, float ury, string url) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 2;
			this.annotationAttributes["file"] = url;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000C9D7B File Offset: 0x000C8D7B
		public Annotation(float llx, float lly, float urx, float ury, string file, string dest) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 3;
			this.annotationAttributes["file"] = file;
			this.annotationAttributes["destination"] = dest;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000C9DB4 File Offset: 0x000C8DB4
		public Annotation(float llx, float lly, float urx, float ury, string moviePath, string mimeType, bool showOnDisplay) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 7;
			this.annotationAttributes["file"] = moviePath;
			this.annotationAttributes["mime"] = mimeType;
			this.annotationAttributes["parameters"] = new bool[]
			{
				default(bool),
				showOnDisplay
			};
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000C9E14 File Offset: 0x000C8E14
		public Annotation(float llx, float lly, float urx, float ury, string file, int page) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 4;
			this.annotationAttributes["file"] = file;
			this.annotationAttributes["page"] = page;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000C9E51 File Offset: 0x000C8E51
		public Annotation(float llx, float lly, float urx, float ury, int named) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 5;
			this.annotationAttributes["named"] = named;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000C9E7C File Offset: 0x000C8E7C
		public Annotation(float llx, float lly, float urx, float ury, string application, string parameters, string operation, string defaultdir) : this(llx, lly, urx, ury)
		{
			this.annotationtype = 6;
			this.annotationAttributes["application"] = application;
			this.annotationAttributes["parameters"] = parameters;
			this.annotationAttributes["operation"] = operation;
			this.annotationAttributes["defaultdir"] = defaultdir;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000C9EE3 File Offset: 0x000C8EE3
		public int Type
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000C9EE8 File Offset: 0x000C8EE8
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x000C9F18 File Offset: 0x000C8F18
		public List<Chunk> Chunks
		{
			get
			{
				return new List<Chunk>();
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000C9F1F File Offset: 0x000C8F1F
		public void SetDimensions(float llx, float lly, float urx, float ury)
		{
			this.llx = llx;
			this.lly = lly;
			this.urx = urx;
			this.ury = ury;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000C9F3E File Offset: 0x000C8F3E
		public float GetLlx()
		{
			return this.llx;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000C9F46 File Offset: 0x000C8F46
		public float GetLly()
		{
			return this.lly;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000C9F4E File Offset: 0x000C8F4E
		public float GetUrx()
		{
			return this.urx;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000C9F56 File Offset: 0x000C8F56
		public float GetUry()
		{
			return this.ury;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000C9F5E File Offset: 0x000C8F5E
		public float GetLlx(float def)
		{
			if (float.IsNaN(this.llx))
			{
				return def;
			}
			return this.llx;
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000C9F75 File Offset: 0x000C8F75
		public float GetLly(float def)
		{
			if (float.IsNaN(this.lly))
			{
				return def;
			}
			return this.lly;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000C9F8C File Offset: 0x000C8F8C
		public float GetUrx(float def)
		{
			if (float.IsNaN(this.urx))
			{
				return def;
			}
			return this.urx;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000C9FA3 File Offset: 0x000C8FA3
		public float GetUry(float def)
		{
			if (float.IsNaN(this.ury))
			{
				return def;
			}
			return this.ury;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000C9FBA File Offset: 0x000C8FBA
		public int AnnotationType
		{
			get
			{
				return this.annotationtype;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x000C9FC2 File Offset: 0x000C8FC2
		public string Title
		{
			get
			{
				if (this.annotationAttributes.ContainsKey("title"))
				{
					return (string)this.annotationAttributes["title"];
				}
				return "";
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x000C9FF1 File Offset: 0x000C8FF1
		public string Content
		{
			get
			{
				if (this.annotationAttributes.ContainsKey("content"))
				{
					return (string)this.annotationAttributes["content"];
				}
				return "";
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000CA020 File Offset: 0x000C9020
		public Dictionary<string, object> Attributes
		{
			get
			{
				return this.annotationAttributes;
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000CA028 File Offset: 0x000C9028
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000CA02B File Offset: 0x000C902B
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000CA02E File Offset: 0x000C902E
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040016F5 RID: 5877
		public const int TEXT = 0;

		// Token: 0x040016F6 RID: 5878
		public const int URL_NET = 1;

		// Token: 0x040016F7 RID: 5879
		public const int URL_AS_STRING = 2;

		// Token: 0x040016F8 RID: 5880
		public const int FILE_DEST = 3;

		// Token: 0x040016F9 RID: 5881
		public const int FILE_PAGE = 4;

		// Token: 0x040016FA RID: 5882
		public const int NAMED_DEST = 5;

		// Token: 0x040016FB RID: 5883
		public const int LAUNCH = 6;

		// Token: 0x040016FC RID: 5884
		public const int SCREEN = 7;

		// Token: 0x040016FD RID: 5885
		public const string TITLE = "title";

		// Token: 0x040016FE RID: 5886
		public const string CONTENT = "content";

		// Token: 0x040016FF RID: 5887
		public const string URL = "url";

		// Token: 0x04001700 RID: 5888
		public const string FILE = "file";

		// Token: 0x04001701 RID: 5889
		public const string DESTINATION = "destination";

		// Token: 0x04001702 RID: 5890
		public const string PAGE = "page";

		// Token: 0x04001703 RID: 5891
		public const string NAMED = "named";

		// Token: 0x04001704 RID: 5892
		public const string APPLICATION = "application";

		// Token: 0x04001705 RID: 5893
		public const string PARAMETERS = "parameters";

		// Token: 0x04001706 RID: 5894
		public const string OPERATION = "operation";

		// Token: 0x04001707 RID: 5895
		public const string DEFAULTDIR = "defaultdir";

		// Token: 0x04001708 RID: 5896
		public const string LLX = "llx";

		// Token: 0x04001709 RID: 5897
		public const string LLY = "lly";

		// Token: 0x0400170A RID: 5898
		public const string URX = "urx";

		// Token: 0x0400170B RID: 5899
		public const string URY = "ury";

		// Token: 0x0400170C RID: 5900
		public const string MIMETYPE = "mime";

		// Token: 0x0400170D RID: 5901
		protected int annotationtype;

		// Token: 0x0400170E RID: 5902
		protected Dictionary<string, object> annotationAttributes = new Dictionary<string, object>();

		// Token: 0x0400170F RID: 5903
		private float llx = float.NaN;

		// Token: 0x04001710 RID: 5904
		private float lly = float.NaN;

		// Token: 0x04001711 RID: 5905
		private float urx = float.NaN;

		// Token: 0x04001712 RID: 5906
		private float ury = float.NaN;
	}
}
