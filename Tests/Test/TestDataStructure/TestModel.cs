namespace XuanLibrary.Test.TestDataStructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using XuanLibrary.Utility;

    public class TestModel
    {
        private string _fieldA;
        private Complex _fieldB;

        public string PropertyA { get; set; }

        public List<int> PropertyB { get; set; }

        public List<Complex> PropertyC { get; set; }

        public Dictionary<string, List<string>> PropertyD { get; set; }

        public PropertyItem PropertyE { get; set; }

        public PropertyItem PropertyF { get; set; }

        public TestModel(string a, List<int> b, List<Complex> c, Dictionary<string, List<string>> d, PropertyItem e, PropertyItem f)
        {
            PropertyA = a;
            PropertyB = b;
            PropertyC = c;
            PropertyD = d;
            PropertyE = e;
            PropertyF = f;
        }
    }

    public class Complex
    {
        public int ComplexPropertyA { get; set; }

        public List<string> ComplexPropertyB { get; set; }
    }

    public class PropertyItem
    {
        public Dictionary<string, Complex> Property { get; set; }
    }

    public class TestModelSerializationCollection : IReadOnlyCollection<KeyValuePair<string, object>>
    {
        private TestModel _model;

        public TestModelSerializationCollection(TestModel model)
        {
            _model = model;
        }

        public int Count => 4;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            yield return new KeyValuePair<string, object>(nameof(_model.PropertyA), _model.PropertyA);
            yield return new KeyValuePair<string, object>(nameof(_model.PropertyB), _model.PropertyB.Cast<object>().ToList());
            yield return new KeyValuePair<string, object>(nameof(_model.PropertyC), JsonUtility.Serialize(_model.PropertyC));
            yield return new KeyValuePair<string, object>(nameof(_model.PropertyD), _model.PropertyD.Select(p => new KeyValuePair<string, object>(p.Key, p.Value)).ToList());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
