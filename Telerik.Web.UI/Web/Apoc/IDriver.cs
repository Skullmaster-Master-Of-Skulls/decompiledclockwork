using System;
using System.IO;
using System.Net;
using System.Xml;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc
{
	// Token: 0x0200136F RID: 4975
	public interface IDriver
	{
		// Token: 0x0600CFA5 RID: 53157
		void Render(XmlDocument doc, Stream outputStream);

		// Token: 0x0600CFA6 RID: 53158
		void Render(TextReader inputReader, Stream outputStream);

		// Token: 0x0600CFA7 RID: 53159
		void Render(string inputFile, string outputFile);

		// Token: 0x0600CFA8 RID: 53160
		void Render(string inputFile, Stream outputStream);

		// Token: 0x0600CFA9 RID: 53161
		void Render(Stream inputStream, Stream outputStream);

		// Token: 0x0600CFAA RID: 53162
		void Render(XmlReader inputReader, Stream outputStream);

		// Token: 0x170042C0 RID: 17088
		// (get) Token: 0x0600CFAB RID: 53163
		// (set) Token: 0x0600CFAC RID: 53164
		IRendererOptions Options { get; set; }

		// Token: 0x170042C1 RID: 17089
		// (get) Token: 0x0600CFAD RID: 53165
		// (set) Token: 0x0600CFAE RID: 53166
		int Timeout { get; set; }

		// Token: 0x170042C2 RID: 17090
		// (get) Token: 0x0600CFAF RID: 53167
		CredentialCache Credentials { get; }

		// Token: 0x170042C3 RID: 17091
		// (get) Token: 0x0600CFB0 RID: 53168
		// (set) Token: 0x0600CFB1 RID: 53169
		DirectoryInfo BaseDirectory { get; set; }

		// Token: 0x170042C4 RID: 17092
		// (get) Token: 0x0600CFB2 RID: 53170
		// (set) Token: 0x0600CFB3 RID: 53171
		RendererEngine Renderer { get; set; }

		// Token: 0x170042C5 RID: 17093
		// (get) Token: 0x0600CFB4 RID: 53172
		// (set) Token: 0x0600CFB5 RID: 53173
		bool CloseOnExit { get; set; }
	}
}
