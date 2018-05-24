using AKA.BusinessLayer.BookService;
using AKA.BusinessLayer.LibraryService;
using AKA.BusinessLayer.MemberService;
using AKA.DataLayer;
using System;

using Unity;

namespace AkaLibraryMVC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<IMemberService, MemberService>();
            container.RegisterType<LibraryContext>();
        }
    }
}