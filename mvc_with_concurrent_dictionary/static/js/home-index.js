'use strict'

const getCookie = name => {
    const value = `; ${document.cookie}`
    const parts = value.split(`; ${name}=`)
    if (parts.length === 2) return parts.pop().split(';').shift()
}

const setCookie = (name, value, days) => {
    let expires = ''
    if (days) {
        const date = new Date()
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000))
        expires = `; expires=${date.toUTCString()}`
    }
    document.cookie = `${name}=${value || ''}${expires}; path=/`
}

const uuidv4 = () => {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
        const r = Math.random() * 16 | 0
        const v = c === 'x' ? r : (r & 0x3 | 0x8)
        return v.toString(16)
    })
}

const refresh_session = () => {
    fetch('/api/dict/session/').then(res => res.json()).then(data => {
        console.log(data);
        const sessions = document.getElementById('sessions');
        sessions.innerHTML = '';
        data.forEach(element => {
            const session = document.createElement('li');
            session.textContent = element;
            sessions.appendChild(session);
        })
    });
}

const refresh_dictvalue = () => {
    // 내 세션값 가져오기
    const session = getCookie('sessionId');

    // 내 세션으로 dict 조회하기
    fetch(`/api/dict/key/?session=${session}`).then(res => res.json()).then(data => {
        console.log(data);
        const dict_vlaues = document.getElementById('dict_value');
        dict_vlaues.innerHTML = '';
        Object.keys(data).forEach(key => {
            const node = document.createElement('li');
            node.textContent = `${key} : ${data[key]}`;
            dict_vlaues.appendChild(node);
        });
    });
}

const input_dictvalue = () => {
    // 내 세션값 가져오기
    const session = getCookie('sessionId');
    // 새 key 가져오기
    const key = document.getElementById('new_dictkey').value;
    // 새 value 가져오기
    const value = document.getElementById('new_dictvalue').value;

    //alert(`${session} : ${key} : ${value}`);
    // POST : api/dict/value/
    // QS : session
    // QS : key
    // QS : value
    const url = `/api/dict/value/?session=${session}&key=${key}&value=${value}`;
    fetch(url, { method: 'POST', mode: "cors", cache: "default" }).then(() => {
        refresh_session();
        refresh_dictvalue();
    });

}

// refresh_session 에 click 이벤트 추가
document.getElementById('refresh_session').addEventListener('click', refresh_session);

// refresh_dictvalue 에 click 이벤트 추가 
document.getElementById('refresh_dictvalue').addEventListener('click', refresh_dictvalue);

// input_dictvalue 에 click이벤트 추가
document.getElementById('input_dictvalue').addEventListener('click', input_dictvalue);

window.onload = () => {
    // 쿠키에 sessionId가 없으면 생성
    if (!getCookie('sessionId')) { setCookie('sessionId', uuidv4(), 1); }

    // 쿠키에 sessionId가 있으면 가져옴. class 이름이 sessionId인 태그에 쿠키값을 넣어줌
    document.getElementsByClassName('sessionId')[0].innerHTML = getCookie('sessionId');

    // 시작시 목록갱신
    refresh_session();
    refresh_dictvalue();
}
