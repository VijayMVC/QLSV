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
                $("#lvEducation").val(result.data[0].CODEVIEW);
                this.getDepartment();
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
        var lveducationCode = $("#lvEducation").val();
        $.ajax({
            url: 'http://localhost:1869/department/getlistbylvCode',
            dataType: 'json',
            data: {
                lveducationCode: lveducationCode
            },
            success: function (data) {
                if (data.ret >= 0) {
                    this.setState({
                        isLoaded: true,
                        Department: data.data
                    });
                }
                else {
                    alert("Lỗi không lấy được dữ liệu");
                }
                this.getSpeciality();
            }.bind(this),
            error: function (xhr, status, err) {
                this.setState({
                    isLoaded: true,
                    error: err
                });
            }.bind(this)
        });
    },
    getSpeciality: function () {
        var departmentCode = $("#department").val();
        $.ajax({
            url: 'http://localhost:1869/SPECIALITY/getListByDepartmentCode',
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
                    error: err
                });
            }.bind(this)
        });
    },
    getNumberYear: function () {
        var codeview = $("#lvEducation").val();
        $.ajax({
            url: 'http://localhost:1869/lveducation/getNumberYear',
            dataType: 'json',
            data: {
                codeview: codeview
            },
            success: function (data) {
                if (data.ret >= 0) {
                    this.setState({
                        isLoaded: true,
                        numberyear: data.data
                    });
                }
                else {
                    alert("Lỗi không lấy được dữ liệu");
                }
                this.getSpeciality();
            }.bind(this),
            error: function (xhr, status, err) {
                this.setState({
                    isLoaded: true,
                    error: err
                });
            }.bind(this)
        });
    },
    checkNumber: function () {
        this.getSpeciality();
        var temp = $("#semester").val();
        if (temp > this.state.numberyear * 2) {
            alert("Nhập sai thông tin kỳ!");
            $("#semester").val("");
        }
    },
    showView: function () {
        var query = null;
        var lveducationCode = $("#lvEducation").val();
        var checked = $('input:radio[name=optradio]:checked').val();
        if (checked == 2) {
            var temp = $("#semester").val();
            if (temp) {
                query = {
                    semester: temp,
                    leveleducation: lveducationCode
                }
            }
            else {
                query = {
                    semester: 1,
                    leveleducation: lveducationCode
                }
            }
        }
        else { // Mặc định chọn theo chuyên ngành 
            var temp = $("#speciality").val();
            query = {
                speciality: temp,
                leveleducation: lveducationCode
            }
        }
        $.ajax({
            url: 'http://localhost:1869/CTDaoTao/GetAllSubject',
            method:'POST',
            dataType: 'json',
            data: query,
            success: function (data) {
                if (data.ret >= 0) {
                    this.setState({
                        isLoaded: true,
                        lstSubject: data.data,
                        message : data.message
                    });
                    $("#viewModal").modal('show');
                }
                else {
                    alert("Lỗi không lấy được dữ liệu");
                }
            }.bind(this),
            error: function (xhr, status, err) {
                this.setState({
                    isLoaded: true,
                    error: err
                });
            }.bind(this)
        });
    },
    //phuong thuc quan trong nhat-->render html la ngoai
    render: function () {
        const { error, isLoaded, lvE, Department, Speciality, numberyear, lstSubject} = this.state;
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
                                <SelectLvEducation loadData={this.getLvEducation} data={this.state.lvE} onChangeSelect={this.getDepartment} setNumberYear={this.getNumberYear}/>
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
                                <input type="radio" name="optradio" value={"1"}  />
                                <span>Xem theo chuyên ngành : </span>
                                <SelectSpeciality loadData={this.getSpeciality} data={this.state.Speciality} />
                            </div>
                            <div className="col-md-4">
                                <input type="radio" name="optradio" value={"2"}  />
                                <span>Xem theo kỳ học : </span>
                                <input type="number" id="semester" className="form-control" onChange={this.checkNumber} />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-4">
                            </div>
                            <div className="col-md-4">
                                <button className="btn-primary form-control" style={{ 'margin-top': '50px' }} onClick={() => this.showView()}>Xem chương trình đào tạo</button>
                            </div>
                           
                        </div>
                    </div>
                    <ShowDetailCourse data={this.state.lstSubject} message={this.state.message} />
                </div>
            );
        }
    }
});
//Tao 1 comportment con để hiển thị danh sách các đơn vị
var SelectLvEducation = React.createClass({
    onChangeDropdown:function(){
        var temp = $("#lvEducation").val();
        this.props.onChangeSelect();
        this.props.setNumberYear();
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

var ShowDetailCourse = React.createClass({
    render: function () {
        var listTable = [];
        var index = 0;
        var check = false;
        if (this.props.data) {
            var lstData = this.props.data;
            this.props.data.forEach(function (obj) {
                //child function so that, this does mean thi window not the react object
                if (obj.PARENTCODE == null) {
                    check = true;
                    index++;
                    listTable.push(<TableDetail data={lstData} parent={obj} index={index} />);
                }
            });
            if (!check) {
                listTable.push(<TableDetailSemester data={lstData} />);
            }
        }
        return (
            <div className="modal fade" id="viewModal" role="dialog" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog modal-lg" role="document">
                    <div className="modal-content ">
                        <div className="modal-header" style={{ 'border-bottom': 'solid 2px #ccc' }}>
                            <button type="button" className="close" data-dismiss="modal"></button>
                            <h4 className="box-title" id="titleOption">{this.props.message}</h4>
                        </div>
                        <div className="modal-body modalScroll">
                            <form className="form-horizontal">
                                <div className="box-body">
                                    {listTable}
                                </div>
                            </form>
                        </div>
                        <button className="btn btn-danger" id="cmdCancel" data-dismiss="modal">
                                Đóng
                        </button>
                    </div>
                </div>
            </div>
        );
    }
});

var TableDetail = React.createClass({
    render: function () {
        var listRow = [];
        var listRowChild = [];
        var index = 0;
        var indexChild = 0;
        if (this.props.parent) 
        {
            var parent = this.props.parent;
            var lstData = this.props.data;
            var datas = this.props.data;
            this.props.data.forEach(function (obj) {
                if (obj.PARENTCODE == parent.CODEVIEW) {
                    index++;
                    listRow.push(<TableRowsParent data={lstData} parent={obj} index={index} />);//lấy cha
                    datas.forEach(function (objChild) {
                        if (obj.CODEVIEW == objChild.PARENTCODE) {
                            indexChild++;
                            listRow.push(<TableRowsParent data={lstData} child={objChild} indexParent={index} index={indexChild} />);
                        }
                    });
                }
            });
        }
        return (
            <div>
                <div style={{ 'font-weight': 'bold' }}>{this.props.index} , {this.props.parent.SUBJECTNAME}</div>
                <table id="viewCT" className="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Tên học phần</th>
                            <th>Số TC</th>
                            <th>Cấu trúc x(a;b)</th>
                            <th>Số tiết TKB</th>
                            <th>Kỳ học</th>
                        </tr>
                    </thead>
                    <tbody>{listRow}</tbody>
                </table>
            </div>
        );
    }
});

var TableRowsParent = React.createClass({
    render: function () {
        if (this.props.parent) {
            var parent = this.props.parent;
            return (
                <tr style={{ 'font-style': 'italic' }}>
                    <td>{this.props.index}</td>
                    <td style={{ 'width':'430px'}}>{parent.SUBJECTNAME}</td>
                    <td>{parent.NUMBEROFCREDIT}</td>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
            );
        }
        else {
            var child = this.props.child;
            var ratio = child.RATIO.split(';');
            if (!this.props.indexParent) {
                this.props.indexParent = 1;
            }
            var soTiet = ratio[0] * 15 + ratio[1] * 30;
            return (
                <tr>
                    <td>{this.props.indexParent}.{this.props.index}</td>
                    <td style={{ 'width': '430px' }}>{child.SUBJECTNAME}</td>
                    <td>{child.NUMBEROFCREDIT}</td>
                    <th>{child.RATIO}</th>
                    <th>{soTiet}</th>
                    <th>{child.EXPECTEDSEMESTER}</th>
                </tr>
            );
        }
    }
});

var TableDetailSemester = React.createClass({
    render: function () {
        var listRow = [];
        var index = 0;
        var lstData = this.props.data;
        this.props.data.forEach(function (obj) {
            index++;
            listRow.push(<TableRowsParent data={lstData} child={obj} index={index} />);//lấy cha
        });
        return (
            <table id="viewCT" className="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Tên học phần</th>
                        <th>Số TC</th>
                        <th>Cấu trúc x(a;b)</th>
                        <th>Số tiết TKB</th>
                        <th>Kỳ học</th>
                    </tr>
                </thead>
                <tbody>{listRow}</tbody>
            </table>
        );
    }
});

//Render html vao the co Id = "container"
var AppRendered = React.render(<App />, document.getElementById("content"));//1 comportment lon nhat
