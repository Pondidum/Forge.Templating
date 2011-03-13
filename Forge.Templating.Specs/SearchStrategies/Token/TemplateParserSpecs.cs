using Machine.Specifications;
using Forge.Templating.SearchStrategies.Token;

namespace Forge.Templating.Specs.SearchStrategies.Token
{

    public class When_passed_a_null_template : SpecBase
    {
        static TemplateParser parser;
        Establish context = () => { };
        Because of = () => ex = Catch.Exception(() => { });
        It should_throw_an_argument_exception = () => { };
    }
}