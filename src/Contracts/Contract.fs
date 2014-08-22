namespace Archient.Contracts

// Documentation is provided in Signature File (.fsi)

module Contract =
    
    let createValueProviderStrategy<'ctx,'t> canProvide getValue =
        {
            new IValueProviderStrategy<'ctx,'t> with
                override me.CanProvideValue(context) = canProvide context
                override me.GetValue(context) = getValue context
        }