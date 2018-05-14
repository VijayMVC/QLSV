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
    getSpeciality: function () {
        var departmentCode = $("#department").val();
        console.log(departmentCode);
        $.ajax({
            url: '/SPECIALITY/getListByDepartmentCode',
            dataType: 'json',
            data: {
                departmentCode: departmentCode
            },
            success: function (data) {
                if (data.ret >= 0) {
                    this.setState({
                        isLoaded: true,
                        Speciality: data.data,
                    });
                }
                else {
                    alert("Lỗi không lấy được dữ liệu");
                }
                //AppRendered.loadData();
                //pagination(data.total, function () {
                //    AppRendered.loadData();
                //});
            }.bind(this),
            error: function (xhr, status, err) {
                this.setState({
                    isLoaded: true,
                    error
                });
            }.bind(this)
        });
    },
    //phuong thuc quan trong nhat-->render html la ngoai
    render: function () {
        this.getLvEducation();
        const { error, isLoaded, lvE, Department, Speciality} = this.state;
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
                                <SelectDepartment loadData={this.getDepartment} data={this.state.Department} onChangeSelect={this.getSpeciality} />
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
                            <div className="col-md-1">
                            </div>
                            <div className="col-md-5">
                                <span>Xem theo chuyên ngành : </span>
                                <SelectSpeciality loadData={this.getSpeciality} data={this.state.Speciality} />
                            </div>
                            <div className="col-md-5">
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
    onChangeDropdown:function(){
        var temp = $("#lvEducation").val();
    },
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
            <select id="lvEducation" className="form-control" onChange={this.onChangeDropdown} style={{ 'max-width': '300px', 'margin-left': '20px' }}>
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
    onChangeDropdown: function () {
        var temp = $("#department").val();
        this.props.onChangeSelect();
    },
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
            <select id="department" className="form-control" onChange={this.onChangeDropdown} style={{ 'max-width': '300px', 'margin-left': '20px' }}>
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

var SelectSpeciality = React.createClass({
    onChangeDropdown: function () {
        var temp = $("#speciality").val();
        this.props.onChangeSelect();
    },
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
                listSelect.push(<SelectDetailSpeciality item={rowitem} index={index} loadData={this.loadData} />);
            });
        }
        return (
            <select id="speciality" className="form-control" onChange={this.onChangeDropdown} style={{ 'max-width': '300px', 'margin-left': '20px' }}>
                {listSelect}
            </select>
        );
    }
});

var SelectDetailSpeciality = React.createClass({
    render: function () {
        return (
            <option value={this.props.item.CODEVIEW}>{this.props.item.SPECIALITYNAME}</option>
        );
    }
});

//Render html vao the co Id = "container"
var AppRendered = React.render(<App />, document.getElementById("content"));//1 comportment lon nhat
