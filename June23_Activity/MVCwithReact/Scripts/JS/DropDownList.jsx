//class dropdl extends React.Component {
//    desert = [
//        {
//            value=1,
//            text ="Gulab Jamun"
//        },
//        {
//            value=2,
//            text ="Basundi"
//        },
//        {
//            value=3,
//            text ="Jalebi"
//        },
//        {
//            value=4,
//            text ="Ras Malai"
//        },
//    ]
//    render() {
//        return (
//            <div>
//                <h2>List of Items</h2>
//                <hr />
//                <select>
//                    <option>Select Desert</option>
//                    {
//                        this.desert.map((displaydesert) =>
//                            <option title={displaydesert.value}>{displaydesert.text}</option>
//                        )
//                    }                   
//                </select>
//            </div>
//        )
//    }
//}
//React.render(<dropdl />, document.getElementById("ddlist"));

Desert = [
    {
        value: 1,
        item: 'Gulab Jamun'
    },
    {
        value: 2,
        item: 'Basundi'
    },
    {
        value: 3,
        item: 'Jalebi'
    },
    {
        value: 4,
        item: 'Ras Malai'
    },
]
var items = Desert.map(desert => {
    return (
        <option>{desert.item}</option>
    )
})
var Myapp = React.createClass({
    render: function () {
        return (
            <div>
                <select>

                    <option>Laddu</option>
                    <option>Kesari</option>
                    <option>Rasagulla</option>

                    {items}

                </select>
            </div>
        )
    }
});
React.render(<Myapp />, document.getElementById("ddl"));