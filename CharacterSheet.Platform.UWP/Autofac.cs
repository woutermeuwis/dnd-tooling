using Autofac;
using CharacterSheet.Core;
using CharacterSheet.Core.Interfaces;
using CharacterSheet.Platform.UWP.Services;

namespace CharacterSheet.Platform.UWP
{
    public class Autofac : BaseAutofac
    {
        protected override void RegisterPlatformTypes(ContainerBuilder builder)
        {
            builder.RegisterType<StorageService>().As<IStorageService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<PlatformService>().As<IPlatformService>();
        }
    }
}
