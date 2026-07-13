class Member {
    constructor(name, dept) {
        this.name = name;
        this.dept = dept;
    }
    getSummary() {
        return `${this.name} works in ${this.dept}`;
    }
}
const m = new Member("Liam", "Engineering");
console.log(m.getSummary());