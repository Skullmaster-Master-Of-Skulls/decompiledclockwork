using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000049 RID: 73
	public interface IPositionTrackingStream
	{
		// Token: 0x06000385 RID: 901
		object GetKnownPositionElement(bool allowApproximateLocation);

		// Token: 0x06000386 RID: 902
		bool HasPositionInformation(object element);
	}
}
