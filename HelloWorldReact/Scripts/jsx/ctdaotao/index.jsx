//var DateTimePicker = require('/Scripts/bootstrap-datetimepicker.js');
//1 comportment lon nhat
var App = React.createClass({
    getInitialState: function () { //Khởi tạo, data=list department
        return { data: [], firsttime: 1 };
    },
    //phuong thuc quan trong nhat-->render html la ngoai
    render: function () {
        return (
            <div className="panel panel-default">
                <div className="panel-heading"><h3>Chương trình đào tạo</h3></div>
                <div className="panel-body">
                    <div className="row">
                        <div className="col-md-6">
                            <label>Bậc học</label>
                            <select>
                                <option value="1">Bậc</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
});
//Tao 1 comportment con để hiển thị danh sách các đơn vị

//Render html vao the co Id = "container"
var AppRendered = React.render(<App />, document.getElementById("content"));//1 comportment lon nhat
