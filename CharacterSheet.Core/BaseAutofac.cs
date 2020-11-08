using Autofac;
using CharacterSheet.Core.Services;
using CharacterSheet.Core.Services.Interface;
using CharacterSheet.Core.ViewModels;
using CharacterSheet.Core.ViewModels.Macro;

namespace CharacterSheet.Core
{
    public abstract class BaseAutofac
    {
        private static IContainer _container;

        public void Setup()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            _container = builder.Build();
        }

        public static T Resolve<T>() => _container.Resolve<T>();

        private void RegisterTypes(ContainerBuilder builder)
        {
            RegisterPlatformTypes(builder);

            // ViewModels
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MacroSelectionViewModel>().AsSelf();
            builder.RegisterType<SpellMacroViewModel>().AsSelf();

            // Service
            builder.RegisterType<SpellMacroService>().As<ISpellMacroService>();
        }

        protected abstract void RegisterPlatformTypes(ContainerBuilder builder);
    }
}
