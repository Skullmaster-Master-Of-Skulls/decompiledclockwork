using System;
using System.Collections.Specialized;
using System.Configuration.Internal;
using System.IO;

namespace System.Configuration
{
	// Token: 0x0200009E RID: 158
	internal class UpdateConfigHost : DelegatingConfigHost
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		internal UpdateConfigHost(IInternalConfigHost host)
		{
			base.Host = host;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001D5E7 File Offset: 0x0001B7E7
		internal void AddStreamname(string oldStreamname, string newStreamname, bool alwaysIntercept)
		{
			if (string.IsNullOrEmpty(oldStreamname))
			{
				return;
			}
			if (!alwaysIntercept && StringUtil.EqualsIgnoreCase(oldStreamname, newStreamname))
			{
				return;
			}
			if (this._streams == null)
			{
				this._streams = new HybridDictionary(true);
			}
			this._streams[oldStreamname] = new StreamUpdate(newStreamname);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001D628 File Offset: 0x0001B828
		internal string GetNewStreamname(string oldStreamname)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(oldStreamname, false);
			if (streamUpdate != null)
			{
				return streamUpdate.NewStreamname;
			}
			return oldStreamname;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001D64C File Offset: 0x0001B84C
		private StreamUpdate GetStreamUpdate(string oldStreamname, bool alwaysIntercept)
		{
			if (this._streams == null)
			{
				return null;
			}
			StreamUpdate streamUpdate = (StreamUpdate)this._streams[oldStreamname];
			if (streamUpdate != null && !alwaysIntercept && !streamUpdate.WriteCompleted)
			{
				streamUpdate = null;
			}
			return streamUpdate;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001D688 File Offset: 0x0001B888
		public override object GetStreamVersion(string streamName)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(streamName, false);
			if (streamUpdate != null)
			{
				return InternalConfigHost.StaticGetStreamVersion(streamUpdate.NewStreamname);
			}
			return base.Host.GetStreamVersion(streamName);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001D6BC File Offset: 0x0001B8BC
		public override Stream OpenStreamForRead(string streamName)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(streamName, false);
			if (streamUpdate != null)
			{
				return InternalConfigHost.StaticOpenStreamForRead(streamUpdate.NewStreamname);
			}
			return base.Host.OpenStreamForRead(streamName);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public override Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(streamName, true);
			if (streamUpdate != null)
			{
				return InternalConfigHost.StaticOpenStreamForWrite(streamUpdate.NewStreamname, templateStreamName, ref writeContext, false);
			}
			return base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001D728 File Offset: 0x0001B928
		public override void WriteCompleted(string streamName, bool success, object writeContext)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(streamName, true);
			if (streamUpdate != null)
			{
				InternalConfigHost.StaticWriteCompleted(streamUpdate.NewStreamname, success, writeContext, false);
				if (success)
				{
					streamUpdate.WriteCompleted = true;
					return;
				}
			}
			else
			{
				base.Host.WriteCompleted(streamName, success, writeContext);
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000874E File Offset: 0x0000694E
		public override bool IsConfigRecordRequired(string configPath)
		{
			return true;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001D768 File Offset: 0x0001B968
		public override void DeleteStream(string streamName)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(streamName, false);
			if (streamUpdate != null)
			{
				InternalConfigHost.StaticDeleteStream(streamUpdate.NewStreamname);
				return;
			}
			base.Host.DeleteStream(streamName);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001D79C File Offset: 0x0001B99C
		public override bool IsFile(string streamName)
		{
			StreamUpdate streamUpdate = this.GetStreamUpdate(streamName, false);
			if (streamUpdate != null)
			{
				return InternalConfigHost.StaticIsFile(streamUpdate.NewStreamname);
			}
			return base.Host.IsFile(streamName);
		}

		// Token: 0x04000366 RID: 870
		private HybridDictionary _streams;
	}
}
