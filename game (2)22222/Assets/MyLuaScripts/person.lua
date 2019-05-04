person = {}

person["name"] = "xiaoming"

person.age = 20

function person.getage()
    print(showpersonname()) 
    return person.age
end

function showpersonname()
    return  "ABC"
end

return person