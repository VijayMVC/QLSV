------------------------------
--Lấy thông tin khoa theo tên khoa
--Lấy danh sách khoa nếu tên khoa rỗng
------------------------------
CREATE PROC GetFacultyList
	@NAME NVARCHAR(200)
AS
IF @NAME = ''
BEGIN
SELECT * FROM FACULTY
END
ELSE
BEGIN
SELECT * FROM FACULTY
WHERE FACULTYNAME LIKE '%'+@NAME+'%'
END

------------------------------
--Lấy thông tin khoa theo mã khoa
------------------------------
CREATE PROC GetFaculty
	@ID VARCHAR(20)
AS
BEGIN
SELECT * FROM FACULTY
WHERE CODEVIEW = @ID
END

------------------------------
--Thêm mới một khoa
------------------------------
CREATE PROC CreateFaculty
	@CODE VARCHAR(10),
	@CODEVIEW VARCHAR(20),
	@NAME NVARCHAR(200),
	@DESC NVARCHAR(200)
AS
BEGIN
INSERT INTO FACULTY
VALUES (@CODE, @CODEVIEW, @NAME, @DESC)
END

------------------------------
--Xóa một khoa
--Cập nhật lại mã khoa ở những bộ môn liên quan
------------------------------
CREATE PROC DeleteFaculty
	@id VARCHAR(20)
AS
BEGIN
DELETE FROM FACULTY WHERE CODEVIEW = @id
END
BEGIN
UPDATE GENRE
SET FACULTYCODE = NULL
WHERE FACULTYCODE = @id
END

------------------------------
--Cập nhật thông tin một khoa
--Cập nhật lại mã khoa ở những bộ môn liên quan
------------------------------
CREATE PROC UpdateFaculty
	@ID VARCHAR(20),
	@NEWID VARCHAR(20),
	@NAME NVARCHAR(200),
	@DESC NVARCHAR(200)
AS
BEGIN
UPDATE FACULTY
SET CODEVIEW = @NEWID, FACULTYNAME = @NAME, FACULTYDESCRIPTTION = @DESC
WHERE CODEVIEW = @ID
END
BEGIN
UPDATE GENRE
SET FACULTYCODE = @NEWID
WHERE FACULTYCODE = @ID
END

------------------------------
--Lấy thông tin bộ môn theo mã khoa
--Lấy danh sách bộ môn nếu mã khoa rỗng
------------------------------
CREATE PROC GetGenreList
	@ID VARCHAR(20)
AS
IF @ID = ''
BEGIN
SELECT * FROM GENRE
END
ELSE
BEGIN
SELECT * FROM GENRE
WHERE FACULTYCODE = @ID
END