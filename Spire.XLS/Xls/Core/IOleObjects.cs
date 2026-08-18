using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x0200058E RID: 1422
	public interface IOleObjects : IList<IOleObject>
	{
		// Token: 0x0600562E RID: 22062
		IOleObject Add(string fileName, Image image, OleLinkType linkType);
	}
}
