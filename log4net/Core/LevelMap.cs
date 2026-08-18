using System;
using System.Collections;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x0200006A RID: 106
	public sealed class LevelMap
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public void Clear()
		{
			this.m_mapName2Level.Clear();
		}

		// Token: 0x170000CA RID: 202
		public Level this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				Level result;
				lock (this)
				{
					result = (Level)this.m_mapName2Level[name];
				}
				return result;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public void Add(string name, int value)
		{
			this.Add(name, value, null);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public void Add(string name, int value, string displayName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("name", name, "Parameter: name, Value: [" + name + "] out of range. Level name must not be empty");
			}
			if (displayName == null || displayName.Length == 0)
			{
				displayName = name;
			}
			this.Add(new Level(value, name, displayName));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000C03C File Offset: 0x0000A23C
		public void Add(Level level)
		{
			if (level == null)
			{
				throw new ArgumentNullException("level");
			}
			lock (this)
			{
				this.m_mapName2Level[level.Name] = level;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C098 File Offset: 0x0000A298
		public LevelCollection AllLevels
		{
			get
			{
				LevelCollection result;
				lock (this)
				{
					result = new LevelCollection(this.m_mapName2Level.Values);
				}
				return result;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public Level LookupWithDefault(Level defaultLevel)
		{
			if (defaultLevel == null)
			{
				throw new ArgumentNullException("defaultLevel");
			}
			Level result;
			lock (this)
			{
				Level level = (Level)this.m_mapName2Level[defaultLevel.Name];
				if (level == null)
				{
					this.m_mapName2Level[defaultLevel.Name] = defaultLevel;
					result = defaultLevel;
				}
				else
				{
					result = level;
				}
			}
			return result;
		}

		// Token: 0x0400018F RID: 399
		private Hashtable m_mapName2Level = SystemInfo.CreateCaseInsensitiveHashtable();
	}
}
