using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebGrease.Css.ImageAssemblyAnalysis
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	public class ImageAssembleException : Exception
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x000782F1 File Offset: 0x000764F1
		public ImageAssembleException()
		{
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000782F9 File Offset: 0x000764F9
		public ImageAssembleException(string message) : base(message)
		{
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00078302 File Offset: 0x00076502
		public ImageAssembleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0007830C File Offset: 0x0007650C
		internal ImageAssembleException(string imageName, string spriteName, string message) : base(message)
		{
			this.ImageName = imageName;
			this.SpriteName = spriteName;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00078323 File Offset: 0x00076523
		internal ImageAssembleException(string imageName, string spriteName, string message, Exception innerException) : base(message, innerException)
		{
			this.ImageName = imageName;
			this.SpriteName = spriteName;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0007833C File Offset: 0x0007653C
		protected ImageAssembleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.ImageName = info.GetString("ImageName");
			this.SpriteName = info.GetString("SpriteName");
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00078376 File Offset: 0x00076576
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0007837E File Offset: 0x0007657E
		public string ImageName { get; private set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00078387 File Offset: 0x00076587
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0007838F File Offset: 0x0007658F
		public string SpriteName { get; private set; }

		// Token: 0x06001485 RID: 5253 RVA: 0x00078398 File Offset: 0x00076598
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("ImageName", this.ImageName);
			info.AddValue("SpriteName", this.SpriteName);
		}
	}
}
