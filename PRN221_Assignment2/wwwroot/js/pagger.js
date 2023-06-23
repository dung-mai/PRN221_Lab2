
function pagger(id, pageindex, totalpage, gap, cid) {
    var containter = document.getElementById(id);
    var result = '';

    //generate button for pageindex
    if (cid === null || cid < 1) {
        if (totalpage < 5) {
            for (let i = 1; i < pageindex; i++) {
                result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
            }
            result += '<button>' + pageindex + '</button>';
            for (let i = pageindex + 1; i <= totalpage; i++) {
                result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
            }
        } else {
            if (pageindex - gap > 1 && pageindex + gap < totalpage) {
                result += '<button type="submit" name="page" value="1">First</button>';
                for (let i = pageindex - gap; i < pageindex; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button>' + pageindex + '</button>';
                for (let i = pageindex + 1; i <= pageindex + gap; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button type="submit" name="page" value="' + (totalpage - 1) + '">Last</button>';
            } else if (pageindex - gap <= 1) {
                for (let i = 1; i < pageindex; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button>' + pageindex + '</button>';
                for (let i = pageindex + 1; i <= 5; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button type="submit" name="page" value="' + (totalpage - 1) + '">Last</button>';
            } else {
                result += '<button type="submit" name="page" value="1">First</button>';

                for (let i = totalpage - 4; i < pageindex; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button>' + pageindex + '</button>';
                for (let i = pageindex + 1; i <= totalpage; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
            }
        }
    } else {
        if (totalpage < 5) {
            for (let i = 1; i < pageindex; i++) {
                result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
            }
            result += '<button>' + pageindex + '</button>';
            for (let i = pageindex + 1; i <= totalpage; i++) {
                result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
            }
        } else {
            if (pageindex - gap > 1 && pageindex + gap < totalpage) {
                result += '<button type="submit" name="page" value="1">First</button>';
                for (let i = pageindex - gap; i < pageindex; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';

                }
                result += '<button>' + pageindex + '</button>';
                for (let i = pageindex + 1; i <= pageindex + gap; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button type="submit" name="page" value="' + (totalpage - 1) + '">Last</button>';
            } else if (pageindex - gap <= 1) {
                for (let i = 1; i < pageindex; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button>' + pageindex + '</button>';
                for (let i = pageindex + 1; i <= 5; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button type="submit" name="page" value="' + (totalpage - 1) + '">Last</button>';
            } else {
                result += '<button type="submit" name="page" value="1">First</button>';
                for (let i = totalpage - 4; i < pageindex; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
                result += '<button>' + pageindex + '</button>';
                for (let i = pageindex + 1; i <= totalpage; i++) {
                    result += '<button type="submit" name="page" value="' + i + '">' + i + '</button>';
                }
            }
        }
    }


    containter.innerHTML = result;
}


