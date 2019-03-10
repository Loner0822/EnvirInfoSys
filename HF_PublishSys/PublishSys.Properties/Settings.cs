using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PublishSys.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;

		[DefaultSettingValue("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\bin\\x86\\Debug\\data\\PublishData.mdb")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		public string PublishDataConnectionString
		{
			get
			{
				return (string)this["PublishDataConnectionString"];
			}
		}
	}
}
