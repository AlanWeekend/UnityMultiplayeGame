-- require("person")
-- print(person.getage())


-- __index是一个表格
-- son = {}
-- parent = {__index = {name = "laowang"}}
-- setmetatable(son, parent)
-- print(son.name)

--__index是一个方法
-- son={}
-- parent={__index = function (tb,key)
--     -- body
--     if key == "name" then
--         -- body
--         return "laowang"
--     else 
--         return "null variable"
--     end
-- end
-- }
-- setmetatable(son,parent)
-- print(son.age)

-- __newindex是一个表格
-- tb = {}
-- newtb = {}
-- setmetatable(tb,{__newindex = newtb})
-- tb.name = "xiaozhang"
-- print(tb.name)
-- print(newtb.name)

--__newindex是一个方法
-- son = {name = "xiaowang"}
-- parent = { __newindex = 

--     function (tb,key,value)
--         if key == "age" then
--             print(key,value)
--         end
--     end
-- }

-- setmetatable(son,parent)

-- son.name = "xiaozhang"
-- son.age = "beijing"

-- tb = {1,2,3}
-- print(tb)
-- -- tb()

-- xiaoming = {name = "xiaoming",age = 18,address = "beijing"}

-- print(xiaoming.name)
-- print(xiaoming.age)
-- print(xiaoming.address)

Person = {name = "xiaoming",age = 19,sex = "M"}
function Person:new(name,age,sex)
    o = {}
    setmetatable(o,self)
    self.__index = self
    self.name = name
    self.age = age
    self.sex = sex
    return o
end

function Person:getsex()
    -- body
    return self.sex
end

xiaoming = Person:new("xiaoming",10,"M")

xiaoming.age = 11

print(xiaoming.age)
print(xiaoming:getsex())

Person.maxPerson = 100000
print(Person.maxPerson)

function Person.getMax()
    -- body
    return  Person.maxPerson
end

print(Person.getMax())

