//var DateTimePicker = require('/Scripts/bootstrap-datetimepicker.js');
//1 comportment lon nhat
var App = React.createClass({
    getInitialState: function () { //Khởi tạo, data=list department
        return { data: [], firsttime: 1 };
    },
    componentWillMount: function () {
        this.getLvEducation();
        this.getDepartment();
    },
    getLvEducation: function () {
        fetch("http://localhost:1869/lveducation/getlist")
            .then(res => res.json())
            .then(
            (result) => {
                this.setState({
                    isLoaded: true,
                    lvE: result.data
                });
            },
            (error) => {
                this.setState({
                    isLoaded: true,
                    error
                });
            }
            )
    },
    getDepartment: function () {
        fetch("http://localhost:1869/department/getlist")
            .then(res => res.json())
            .then(
            (result) => {
                this.setState({
                    isLoaded: true,
                    Department: result.data
                });
            },
            (error) => {
                this.setState({
                    isLoaded: true,
                    error
                });
            }
            )
    },
    //phuong thuc quan trong nhat-->render html la ngoai
    render: function () {
        this.getLvEducation();
        const { error, isLoaded, lvE, Department } = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Loading...</div>;
        } else {
            return (
                <div className="panel panel-default">
                    <div className="panel-heading"><h3 style={{ 'font-size': '20px' , 'color':'red' ,'font-weight':'bold'}}>Chương trình đào tạo</h3></div>
                    <div className="panel-body">
                        <div className="row">
                            <div className="col-md-1">
                            </div>
                            <div className="col-md-5">
                                <label style={{'font-size': '20px' }}>Bậc học : </label>
                                <SelectLvEducation loadData={this.getLvEducation} data={this.state.lvE} />
                            </div>
                            <div className="col-md-5">
                                <label style={{ 'font-size': '20px' }}>Ngành học : </label>
                                <SelectDepartment loadData={this.getDepartment} data={this.state.Department} />
                            </div>
                        </div>
                        <div className="row" style={{ 'margin-top': '30px' }}>
                            <div className="col-md-4">
                            </div>
                            <div className="col-md-4">
                                <span style={{ 'font-size': '20px' }}>Xem chương trình đào tạo theo </span>
                                <span className="fa fa-cubes"></span>
                            </div>
                        </div>
                        <div className="row">
                           <hr/>
                        </div>
                        <div className="row" style={{ 'margin-top': '30px' }}>
                            <div className="col-md-4">
                            </div>
                            <div className="col-md-4">
                                <span style={{ 'font-size': '20px' }}>Xem chương trình đào tạo theo </span>
                                <span className="fa fa-cubes"></span>
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
    }
});
//Tao 1 comportment con để hiển thị danh sách các đơn vị
var SelectLvEducation = React.createClass({
    loadData: function () {
        this.props.loadData();
    },
    render: function () {
        var index = 0;
        var listSelect = [];
        if (this.props.data) {
            this.props.data.forEach(function (rowitem) {
                //child function so that, this does mean thi window not the react object
                index++;
                listSelect.push(<SelectDetailLVE item={rowitem} index={index} loadData={this.loadData}/>);
            });
        }
        return (
            <select id="lvEducation" className="form-control" style={{ 'max-width': '300px', 'margin-left': '20px' }}>
                {listSelect}
            </select>
        );
    }
});

var SelectDetailLVE = React.createClass({
    render: function () {
        return (
            <option value={this.props.item.CODEVIEW}>{this.props.item.LEVELNAME}</option>
        );
    }
});

var SelectDepartment = React.createClass({
    loadData: function () {
        this.props.loadData();
    },
    render: function () {
        var index = 0;
        var listSelect = [];
        if (this.props.data) {
            this.props.data.forEach(function (rowitem) {
                //child function so that, this does mean thi window not the react object
                index++;
                listSelect.push(<SelectDetailDepartment item={rowitem} index={index} loadData={this.loadData} />);
            });
        }
        return (
            <select id="lvEducation" className="form-control" style={{ 'max-width': '300px', 'margin-left': '20px' }}>
                {listSelect}
            </select>
        );
    }
});

var SelectDetailDepartment = React.createClass({
    render: function () {
        return (
            <option value={this.props.item.CODEVIEW}>{this.props.item.DEPARTMENTNAME}</option>
        );
    }
});
//Render html vao the co Id = "container"
var AppRendered = React.render(<App />, document.getElementById("content"));//1 comportment lon nhat
