-- 单行注释print("hello world -- lua")
--[[print("AB
    ewfsdsd
    多行注释
CD")]]

-- xiaoming = "Xiaoming"
-- a = 10.2
-- isShout = true
-- print(type(isShout))

-- --在table中存储单个变量
-- mytable = {1,2,3,4,5,6,2227,7,34,"aldjlfj",true,{1,2,3}}
-- --访问
-- print(#mytable)

--在table中存储key-value形式的一对变量
-- newtable = {name = "xiaoming",age = 18,address = "beijing"}
-- --访问
-- print(newtable["name"])
-- print(newtable["age"])
-- print(newtable["address"])
-- print("-----------")
-- print(newtable.name)
-- print(newtable.age)
-- print(newtable.address)
-- print("-----------")
-- --在table中又存储单个变量，又有一对对的k-v
-- mynewtable = {1,2,3,name="xiaohong",5,6,7,age=20}
-- --访问
-- print(mynewtable.name)
-- print(mynewtable[1])
-- print(mynewtable.age)
-- print("------------")

-- school = {
--     1,2,3,
--     student = {
--         "xiaohong","xiaoming","xiaozhang",
--         Tom=
--         {
--             name = "Tom",
--             age = 28,
--         }
--     }
-- }

-- print(school.student.Tom.age)

-- function showmyname(name)
--     -- body
--     local abc = 123
--     print(name)
-- end

-- showmyname(999)
-- print(abc)
-- a,b,c = 1,2,3

-- a,b = 10,20,203,3,3,3,3,3,3

-- print(a)
-- print(b)
-- print(c)

-- function showmyname( a,b,c )
--     -- body
--     print(a)
--     print(b)
--     print(c)
-- end

-- showmyname(1,2,2,2,2,2,2,2)

-- mytable = {2,3,24,55,3}

-- if mytable[3] < 10 then
--     -- body
--     print(mytable[3])
-- end
-- i=10
-- while (i>0)
-- do
--     print(i)
--     i=i-1
-- end

-- for i=1,10 do
--     for j=1,20 do
--         print(i+j)
--     end
-- end

-- mytable = {3,nil,5,6,7,name="xiaoming",age=19,8,9,10,[100]=20}
-- newtable = {name = "xiaohong",age = 18}
-- mynewtable = {[2] = 10,[10]=100}

-- for k,v in pairs(mytable) do
--     print(k,v)
-- end

-- i = 100
-- repeat 
--     print(i)
--     i=i+1
--     if i > 105 then
--         break
--     end
-- until(i>110)

-- lua函数 可以返回多个结果

-- function demofun( a,b,c )
--     return b,a,c
-- end

-- num01,num02,num03,num04 = demofun(1,2,3)

-- print(num01,num02,num03,num04)

-- function demofun( ... )
--     -- body
--     print("可变参数共计" .. select("#", ...))
--     print("可变参数的第3个参数的值是" .. select(3,...))
--     for i,v in ipairs({...}) do
--         print(i,v)
--     end
-- end

-- demofun(1,3,5,6,7)

-- print(3 and 5)

-- function demofun( a,b,c )
--     -- body

--     a = a or 0
--     b = b or 0
--     c = c or 0

--     result = a+b+c

--     print(result)
-- end

-- demofun(1)

-- name = "ljjsldjf"
-- print(string.upper(name))

tb = {}

tb[100]=100
print(tb[100])

myFunc = function ( ... )
    -- body

    print("myFunc")
end

if(nil)
then 
    print(123)
end