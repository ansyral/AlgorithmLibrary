namespace XuanLibrary.Fx.MEFContainer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    public class Container
    {
        private CompositionContainer _container;

        [ImportMany(nameof(Container), typeof(IPrinter))]
        public IEnumerable<Lazy<IPrinter, IPrinterMetadata>> PluggedPrinters { get; set; }

        public Container(string pluginFolder)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(pluginFolder));
            _container = new CompositionContainer(catalog);
            try
            {
                _container.ComposeParts(this);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
